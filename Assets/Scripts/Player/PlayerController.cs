using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 1.0f;

    private CameraSelector _cameraSelector;
    private Vector3 _input;
    private Rigidbody _rigidbody;
    private ThirdPersonCamera _camera;

    public int roomId = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _cameraSelector = FindObjectOfType<CameraSelector>();
    }
    
    void Update()
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
            _input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        }
    }

    private void FixedUpdate()
    {
        if (!_cameraSelector.isScreenSelectorEnabled && _camera && _camera.gameObject.activeInHierarchy)
        {
            _rigidbody.MovePosition(_rigidbody.position + _input * _movementSpeed * Time.fixedDeltaTime);
        }
    }
}
