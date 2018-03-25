using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{

	private enum Direction
	{
		Up = 0,
		Right = 1,
		Down = 2,
		Left = 3
	}


	public bool ReceiveInput = true;
	
	private Direction _direction = Direction.Up;
	private Direction _nextDirection = Direction.Up;
	private bool _moving = false;
	private float _sinceStartOfDirection = 0.0f;

	private Vector2 _currentTile;
	private Vector2 _designatedTile;

	private Animator _animator;
	private readonly Vector2 _offset = new Vector2(0.5f, 0.7f);

	private readonly float _velocity = 3.0f;
	private bool _wantToStop;

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
		_currentTile = new Vector2(
			Mathf.Round(transform.position.x - _offset.x),
			Mathf.Round(transform.position.y - _offset.y)
			);
		_designatedTile = _currentTile;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (!ReceiveInput) return;
		
		var wantToMove = false;
		if (Input.GetAxis("Horizontal") > 0.1) {
			wantToMove = SetDirection(Direction.Right);
		} else if (Input.GetAxis("Horizontal") < -0.1) {
			wantToMove = SetDirection(Direction.Left);
		}

		if (Input.GetAxis("Vertical") > 0.1) {
			wantToMove = SetDirection(Direction.Up);
		} else if (Input.GetAxis("Vertical") < -0.1) {
			wantToMove = SetDirection(Direction.Down);
		}

		if (wantToMove == false) {
			_sinceStartOfDirection = 0.0f;
			if (_currentTile == _designatedTile)
				_moving = false;
		}


		if (_sinceStartOfDirection > 0.1 && _currentTile == _designatedTile) {
			_moving = true;
			if (NextTileIsAccessible()) {
				_designatedTile = _currentTile + DirectionToVector2(_nextDirection);
			}
		}

		if (_designatedTile != _currentTile) {
			var distanceFromDesignatedTile = ((Vector2) transform.position - _offset - _designatedTile);
			var directionComponent = GetComponent(distanceFromDesignatedTile, DirectionToVector2(_direction));
			
			if (directionComponent > 0.0f) {
                _currentTile = _designatedTile;
                _direction = _nextDirection;
                if (wantToMove && NextTileIsAccessible()) {
                    transform.position += (Vector3) DirectionToVector2(_direction) * _velocity * Time.fixedDeltaTime;
                    _designatedTile = _currentTile + DirectionToVector2(_direction);
                } else {
					transform.position = (Vector3) (_designatedTile + _offset);
				}

				if (wantToMove == false)
                    _moving = false;
			} else {
				transform.position += (Vector3) DirectionToVector2(_direction) * _velocity * Time.fixedDeltaTime;
			}
		} else {
			_direction = _nextDirection;
		}

		

		_animator.SetInteger("Direction", (int)_direction);
		_animator.SetBool("Moving", _moving);

	}

	private bool NextTileIsAccessible()
	{
		var currentDirection = DirectionToVector2(_direction);
		RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, currentDirection);
		if (hit.collider != null && hit.transform.CompareTag("Block")) {
			switch (_direction) {
				case Direction.Up:
				case Direction.Right:
                    if (GetComponent(hit.point - _currentTile, currentDirection) < 1.5f) {
                        return false;
                    }
					break;
				case Direction.Left:
				case Direction.Down:
                    if (GetComponent(hit.point - _currentTile, currentDirection) < 0.5f) {
                        return false;
                    }
					break;
			}
		}
		return true;
	}

	private bool SetDirection(Direction direction)
	{
        _sinceStartOfDirection += Time.fixedDeltaTime;
        _nextDirection = direction;

		return true;
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
}
