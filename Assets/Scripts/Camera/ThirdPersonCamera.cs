using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private CameraSelector _cameraSelector;

    public Transform target;
    public float distance = 5.0f;
    public float height = 5.0f;

    void Start()
    {
        _cameraSelector = FindObjectOfType<CameraSelector>();
    }
    
    void LateUpdate()
    {
        if (target && !_cameraSelector.isScreenSelectorEnabled)
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y + height, target.transform.position.z - distance);

            Vector3 targetLookAt = new Vector3(transform.position.x, target.position.y, target.position.z);
            transform.LookAt(targetLookAt);
        }
    }
    
    public void ResizeViewport(float x, float y, float w, float h)
    {
        Camera camera = GetComponent<Camera>();
        camera.rect = new Rect(x, y, w, h);
    }
}