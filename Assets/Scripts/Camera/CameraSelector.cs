using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    private List<ThirdPersonCamera> _cameras = new List<ThirdPersonCamera>();
    private bool _screenSelectorEnabled = false;
    private ThirdPersonCamera _lastActiveCamera = null;

    public bool isScreenSelectorEnabled
    {
        get { return _screenSelectorEnabled; }
    }

    public ThirdPersonCamera lastActiveCamera
    {
        get { return _lastActiveCamera; }
    }

    public void EnableSelector()
    {
        if ( _cameras.Count > 1)
        {
            _screenSelectorEnabled = true;
            int modulo = _cameras.Count % 2;
            for (int i = 0; i < _cameras.Count; i++)
            {
                ThirdPersonCamera camera = _cameras[i].GetComponent<ThirdPersonCamera>();
                if (modulo == 0)
                {
                    camera.ResizeViewport(i / (float)_cameras.Count, (2 / _cameras.Count) == 1 ? 0.0f : 0.5f, 0.5f, 2.0f / (float)_cameras.Count);
                }
                else
                {
                    if (_cameras.Count == 3)
                    {
                        if (i == 0)
                        {
                            camera.ResizeViewport(0.0f, 0.5f, 0.5f, 0.5f);
                        }
                        if (i == 1)
                        {
                            camera.ResizeViewport(0.5f, 0.5f, 0.5f, 0.5f);
                        }
                        if (i == 2)
                        {
                            camera.ResizeViewport(0.25f, 0.0f, 0.5f, 0.5f);
                        }
                    }
                    // 5, which is max
                    else
                    {
                        if (i == 0)
                        {
                            camera.ResizeViewport(0.0f, 0.66f, 0.5f, 0.33f);
                        }
                        if (i == 1)
                        {
                            camera.ResizeViewport(0.5f, 0.66f, 0.5f, 0.33f);
                        }
                        if (i == 2)
                        {
                            camera.ResizeViewport(0.25f, 0.33f, 0.5f, 0.33f);
                        }
                        if (i == 3)
                        {
                            camera.ResizeViewport(0.0f, 0.0f, 0.5f, 0.33f);
                        }
                        if (i == 4)
                        {
                            camera.ResizeViewport(0.5f, 0.0f, 0.5f, 0.33f);
                        }
                    }
                }
                _cameras[i].gameObject.SetActive(true);
            }
            Cursor.visible = true;
        }
    }

    void Start()
    {
        ThirdPersonCamera mainCamera = FindObjectOfType<ThirdPersonCamera>();
        _cameras.Add(mainCamera);
        _lastActiveCamera = mainCamera;
        Cursor.visible = false;
    }

    public void AddCamera(ThirdPersonCamera camera)
    {
        _cameras.Add(camera);
    }

    public bool IsActive(ThirdPersonCamera camera)
    {
        foreach (ThirdPersonCamera activeCamera in _cameras)
        {
            if (activeCamera == camera)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveDisabledCameras()
    {
        foreach (ThirdPersonCamera camera in _cameras)
        {
            if (!camera.gameObject.activeInHierarchy)
            {
                camera.target.gameObject.SetActive(false);
            }
        }
        _cameras.RemoveAll(camera => !camera.gameObject.activeInHierarchy);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !_screenSelectorEnabled)
        {
            EnableSelector();
        }

        if (_screenSelectorEnabled)
        {
            ShowScreenSelector();
        }
    }

    private void ShowScreenSelector()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int indexOfselected = -1;
            float positionX = Input.mousePosition.x / Screen.width;
            float positionY = Input.mousePosition.y / Screen.height;

            if (_cameras.Count == 2)
            {
                if (positionX > 0.5f)
                {
                    indexOfselected = 1;
                }
                else
                {
                    indexOfselected = 0;
                }
            }
            if (_cameras.Count == 3)
            {
                if (positionY > 0.5)
                {
                    if (positionX > 0.5f)
                    {
                        indexOfselected = 1;
                    }
                    else
                    {
                        indexOfselected = 0;
                    }
                }
                else
                {
                    if (positionX > 0.25f && positionX < 0.75f)
                    {
                        indexOfselected = 2;
                    }
                }
            }
            if (_cameras.Count == 4)
            {
                if (positionY > 0.5)
                {
                    if (positionX > 0.5f)
                    {
                        indexOfselected = 1;
                    }
                    else
                    {
                        indexOfselected = 0;
                    }
                }
                else
                {
                    if (positionX < 0.5f)
                    {
                        indexOfselected = 2;
                    }
                    else
                    {
                        indexOfselected = 3;
                    }
                }
            }
            if (_cameras.Count == 5)
            {
                if (positionY > 0.66f)
                {
                    if (positionX > 0.5f)
                    {
                        indexOfselected = 1;
                    }
                    else
                    {
                        indexOfselected = 0;
                    }
                }
                else if (positionY > 0.33f)
                {
                    if (positionX > 0.33f && positionX < 0.66f)
                    {
                        indexOfselected = 2;
                    }
                }
                else
                {
                    if (positionX > 0.5f)
                    {
                        indexOfselected = 4;
                    }
                    else
                    {
                        indexOfselected = 3;
                    }
                }
            }

            if (indexOfselected >= 0)
            {
                for (int i = 0; i < _cameras.Count; i++)
                {
                    ThirdPersonCamera tpCamera = _cameras[i].GetComponent<ThirdPersonCamera>();
                    tpCamera.ResizeViewport(0.0f, 0.0f, 1.0f, 1.0f);
                    if (i != indexOfselected)
                    {
                        _cameras[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        _lastActiveCamera = _cameras[i];
                    }
                }
                Cursor.visible = false;
                _screenSelectorEnabled = false;
            }
        }
    }
}
