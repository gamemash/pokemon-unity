using NUnit.Framework.Internal;
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

	private Direction _direction = Direction.Up;
	private bool _moving = false;
	private float _sinceStartOfDirection = 0.0f;

	private Vector2 _currentTile = new Vector2();
	private Vector2 _designatedTile = new Vector2();

	private Animator _animator;
	private Vector2 _offset = new Vector2(0.5f, 0.7f);

	private float _velocity = 2.0f;
	private bool _wantToStop;

	// Use this for initialization
	void Start ()
	{

		_animator = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
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
		}


		if (_sinceStartOfDirection > 0.1) {
			_moving = true;
			_designatedTile = _currentTile + DirectionToVector2(_direction);
		}

		if (_designatedTile != _currentTile) {
			var distanceFromDesignatedTile = ((Vector2) transform.position - _offset - _designatedTile);
			var directionComponent = GetComponent(distanceFromDesignatedTile, DirectionToVector2(_direction));
			
			if (directionComponent > 0.0f) {
				transform.position = (Vector3)(_designatedTile + _offset);
				_currentTile = _designatedTile;
				if (wantToMove == false)
                    _moving = false;
			} else {
				transform.position += (Vector3) DirectionToVector2(_direction) * _velocity * Time.fixedDeltaTime;
				Debug.Log(Vector3.left * _velocity * Time.fixedDeltaTime);
			}
		}

		

		_animator.SetInteger("Direction", (int)_direction);
		_animator.SetBool("Moving", _moving);

	}

	private bool SetDirection(Direction direction)
	{
		if (_designatedTile == _currentTile) {
			_sinceStartOfDirection += Time.fixedDeltaTime;
			if (direction != _direction) {
                _direction = direction;
				_animator.SetTrigger("ChangeDirection");

			}
		}

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
