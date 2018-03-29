using System.Collections;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public bool ReceiveInput = true;

    private Direction _direction = Direction.Up;

    private Vector2 _currentTile;
    private Vector2 _designatedTile;

    private Animator _animator;
    private readonly Vector2 _offset = new Vector2(0.5f, 0.7f);

    private readonly float _velocity = 3.0f;
    private float _timeSinceDirectionChange = 0.0f;

    private bool _standingStill = true;
    private bool _wantsToMove = false;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        _currentTile = new Vector2(
            Mathf.Round(transform.position.x - _offset.x),
            Mathf.Round(transform.position.y - _offset.y)
        );
        _designatedTile = _currentTile;
    }
     
    // Update is called once per frame
    void Update()
    {
        _wantsToMove = DetermineInput();
        
        if (_wantsToMove && _standingStill && NextTileIsAccessible()) {
            _designatedTile = _currentTile + DirectionToVector2(_direction);
            StartCoroutine(Movement(_designatedTile, _currentTile));
        } 
        
        if (!_wantsToMove && _standingStill) {
            _animator.SetInteger("Direction", (int) _direction);
            _animator.SetBool("Moving", false);
        }
        
    }

    bool DetermineInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0.1) {
            return SetDirection(Direction.Right);
        } else if (Input.GetAxisRaw("Horizontal") < -0.1) {
            return SetDirection(Direction.Left);
        }

        if (Input.GetAxisRaw("Vertical") > 0.1) {
            return SetDirection(Direction.Up);
        } else if (Input.GetAxisRaw("Vertical") < -0.1) {
            return SetDirection(Direction.Down);
        }

        return false;
    }

    private bool NextTileIsAccessible()
    {
        var tile = GetTileInFront();
        if (tile != null) {
            if (tile.CompareTag("Block")) {
                return false;
            } else if (tile.CompareTag("Pathway")) {
                tile.GetComponent<Entrance>().GoThrough();
                return false;
            }
        }

        return true;
    }
    
    IEnumerator Movement (Vector2 targetTile, Vector2 currentTile)
    {
        _animator.SetBool("Moving", true);
        _animator.SetInteger("Direction", (int) _direction);
        _standingStill = false;
        
        while(GetComponent(targetTile - ((Vector2)transform.position - _offset),DirectionToVector2(_direction)) > 0.0f)
        {
            Vector2 position = transform.position;
            var deltaPos = (targetTile - currentTile).normalized * _velocity * Time.fixedDeltaTime;
            transform.position = position + deltaPos;
            
            yield return null;
        }

        transform.position = targetTile + _offset;

        _standingStill = true;
        _currentTile = targetTile;
    }


    public void MoveForward(int steps)
    {
        _designatedTile = _currentTile + DirectionToVector2(_direction) * steps;
    }

    public GameObject GetTileInFront()
    {
        var currentDirection = DirectionToVector2(_direction);
        RaycastHit2D hit = Physics2D.Raycast((Vector2) transform.position, currentDirection);
        if (hit.collider != null) {
            switch (_direction) {
                case Direction.Up:
                case Direction.Right:
                    if (GetComponent(hit.point - _currentTile, currentDirection) < 1.5f) {
                        return hit.collider.gameObject;
                    }

                    break;
                case Direction.Left:
                case Direction.Down:
                    if (GetComponent(hit.point - _currentTile, currentDirection) < 0.5f) {
                        return hit.collider.gameObject;
                    }

                    break;
            }
        }

        return null;
    }


    public bool SetDirection(Direction direction)
    {
        if (direction != _direction)
            _timeSinceDirectionChange = 0.0f;
        
        _direction = direction;
        _timeSinceDirectionChange += Time.deltaTime;
        return _timeSinceDirectionChange > 0.1f;
    }

    private Vector2 DirectionToVector2(Direction direction)
    {
        switch (_direction) {
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            case Direction.Up:
                return Vector2.up;
            default:
                return Vector2.down;
        }
    }

    private float GetComponent(Vector2 a, Vector2 b)
    {
        if (a.x * b.x == 0.0f) {
            return a.y * b.y;
        } else {
            return a.x * b.x;
        }
    }

    public void SetPosition(Vector2 position)
    {
        _currentTile = _designatedTile = position;
        transform.position = position + _offset;
    }
}