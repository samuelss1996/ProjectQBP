using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private CameraSelector _cameraSelector;
    private float _tempDistance = 5.0f;
    private Vector3 _nonCollidingPosition;
    private bool _hitting = false;

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;
    
    float x = 0.0f;
    float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        _tempDistance = distance;
        _nonCollidingPosition = transform.position;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
        _cameraSelector = FindObjectOfType<CameraSelector>();
    }
    
    void LateUpdate()
    {
        if (target && !_cameraSelector.isScreenSelectorEnabled)
        {

            x += Input.GetAxis("Mouse X") * xSpeed * _tempDistance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            _tempDistance = Mathf.Clamp(_tempDistance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, _nonCollidingPosition, out hit))
            {
                if (!_hitting)
                {
                    _tempDistance -= hit.distance;
                    _hitting = true;
                }
            }
            else
            {
                _hitting = false;
                _tempDistance = distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -_tempDistance);
            Vector3 position = rotation * negDistance + target.position;
            _nonCollidingPosition = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;
            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void ResizeViewport(float x, float y, float w, float h)
    {
        Camera camera = GetComponent<Camera>();
        camera.rect = new Rect(x, y, w, h);
    }
}
