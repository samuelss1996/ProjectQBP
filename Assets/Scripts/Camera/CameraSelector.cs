using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelector : MonoBehaviour
{
    public GameObject[] cameras;
    private bool _screenSelectorEnabled = false;
    
    public bool isScreenSelectorEnabled
    {
        get { return _screenSelectorEnabled; }
    }

    void Start()
    {
        cameras[0].SetActive(true);
        Cursor.visible = false;
    }
   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!_screenSelectorEnabled)
            {
                _screenSelectorEnabled = true;
                int modulo = cameras.Length % 2;
                for (int i = 0; i < cameras.Length; i++)
                {
                    ThirdPersonCamera camera = cameras[i].GetComponent<ThirdPersonCamera>();
                    if (modulo == 0)
                    {
                        camera.ResizeViewport(i / (float)cameras.Length, (2 / cameras.Length) == 1 ? 0.0f : 0.5f, 0.5f, 2.0f / (float)cameras.Length);
                    }
                    else
                    {
                        if (cameras.Length == 3)
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
                    cameras[i].SetActive(true);
                }
                Cursor.visible = true;
            }
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

            if (cameras.Length == 2)
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
            if (cameras.Length == 3)
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
            if (cameras.Length == 4)
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
            if (cameras.Length == 5)
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
                for (int i = 0; i < cameras.Length; i++)
                {
                    ThirdPersonCamera tpCamera = cameras[i].GetComponent<ThirdPersonCamera>();
                    tpCamera.ResizeViewport(0.0f, 0.0f, 1.0f, 1.0f);
                    if (i != indexOfselected)
                    {
                        cameras[i].SetActive(false);
                    }
                }
                Cursor.visible = false;
                _screenSelectorEnabled = false;
            }
        }
    }
}
