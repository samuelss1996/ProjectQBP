using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    private CameraSelector _cameraSelector;

    public Transform target;
    public float distance = 5.0f;

    void Start()
    {
        _cameraSelector = FindObjectOfType<CameraSelector>();
    }
    
    void LateUpdate()
    {
        if (target && !_cameraSelector.isScreenSelectorEnabled)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, target.transform.position.z - distance);
        }
    }
    
    public void ResizeViewport(float x, float y, float w, float h)
    {
        Camera camera = GetComponent<Camera>();
        camera.rect = new Rect(x, y, w, h);
    }
}
