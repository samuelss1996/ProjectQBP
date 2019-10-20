using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    [SerializeField]
    private float _movementSpeed = 1.0f;

    [SerializeField]
    private float _rotationSpeed = 0.3f;

    [SerializeField]
    private bool _hasNewDirection = false;

    private float _rotationProgress = 0.0f;

    private CameraSelector _cameraSelector;
    private ThirdPersonCamera _camera;

    private Direction _newDirection = Direction.North;
    private Direction _direction = Direction.North;

    private Quaternion _prevAngle = Quaternion.identity;

    private Rigidbody _rigidbody;
    private Vector3 _input;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private Quaternion FromDirectionToAngle(Direction direction)
    {
        switch (_direction)
        {
            case Direction.North:
                return Quaternion.Euler(0.0f, 0.0f, 0.0f);
            case Direction.NorthEast:
                return Quaternion.Euler(0.0f, 45.0f, 0.0f);
            case Direction.East:
                return Quaternion.Euler(0.0f, 90.0f, 0.0f);
            case Direction.SouthEast:
                return Quaternion.Euler(0.0f, 135.0f, 0.0f);
            case Direction.South:
                return Quaternion.Euler(0.0f, 180.0f, 0.0f);
            case Direction.SouthWest:
                return Quaternion.Euler(0.0f, -135.0f, 0.0f);
            case Direction.West:
                return Quaternion.Euler(0.0f, -90.0f, 0.0f);
            case Direction.NorthWest:
                return Quaternion.Euler(0.0f, -45.0f, 0.0f);
            default:
                break;
        }

        return Quaternion.identity;
    }

    private void Update()
    {
        if (!_camera)
        {
            ThirdPersonCamera[] cameras = FindObjectsOfType<ThirdPersonCamera>();
            foreach (ThirdPersonCamera camera in cameras)
            {
                if (camera.target.gameObject == gameObject)
                {
                    _camera = camera;
                    break;
                }
            }
        }

        if (!_cameraSelector.isScreenSelectorEnabled && _camera && _camera.gameObject.activeInHierarchy)
        {
            bool isMovingHorizontally = Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f;
            bool isMovingVertically = Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < -0.1f;

            Direction newDirection = _direction;

            if (isMovingHorizontally && isMovingVertically)
            {
                if (Input.GetAxis("Horizontal") > 0.1f && Input.GetAxis("Vertical") > 0.1f)
                {
                    newDirection = Direction.NorthEast;
                }
                else if (Input.GetAxis("Horizontal") < -0.1f && Input.GetAxis("Vertical") > 0.1f)
                {
                    newDirection = Direction.NorthWest;
                }
                else if (Input.GetAxis("Horizontal") > 0.1f && Input.GetAxis("Vertical") < -0.1f)
                {
                    newDirection = Direction.SouthEast;
                }
                else if (Input.GetAxis("Horizontal") < -0.1f && Input.GetAxis("Vertical") < -0.1f)
                {
                    newDirection = Direction.SouthWest;
                }
            }
            else if (isMovingHorizontally)
            {
                if (Input.GetAxis("Horizontal") > 0.1f)
                {
                    newDirection = Direction.East;
                }
                else if (Input.GetAxis("Horizontal") < -0.1f)
                {
                    newDirection = Direction.West;
                }
            }
            else if (isMovingVertically)
            {
                if (Input.GetAxis("Vertical") > 0.1f)
                {
                    newDirection = Direction.North;
                }
                else if (Input.GetAxis("Vertical") < -0.1f)
                {
                    newDirection = Direction.South;
                }
            }

            if (newDirection != _direction)
            {
                _hasNewDirection = true;
                _newDirection = newDirection;
            }


            _input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            _input.Normalize();
        }
    }

    private void FixedUpdate()
    {
        if (_hasNewDirection)
        {
            _rotationProgress = 0.0f;
            _prevAngle = _rigidbody.rotation;
            _hasNewDirection = false;
            _direction = _newDirection;
        }

        if (_rotationSpeed <= float.Epsilon)
        {
            _rotationProgress = 1.0f;
        }
        else
        {
            _rotationProgress += Time.fixedDeltaTime / _rotationSpeed;
        }


        if (!_cameraSelector.isScreenSelectorEnabled && _camera && _camera.gameObject.activeInHierarchy)
        {
            _rigidbody.MoveRotation(Quaternion.Slerp(_prevAngle, FromDirectionToAngle(_direction), Mathf.SmoothStep(0.0f, 1.0f, Mathf.Min(1.0f, _rotationProgress))));
            _rigidbody.MovePosition(_rigidbody.position + _input * _movementSpeed * Time.fixedDeltaTime);
        }
    }
}
