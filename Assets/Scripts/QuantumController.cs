using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumController : MonoBehaviour
{
    [SerializeField]
    private int _maxNumberOfStates = 2;
    [SerializeField]
    private ThirdPersonCamera[] _cameras;
    
    private int _numberOfActiveStates = 1;
    private CameraSelector _cameraSelector;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _cameras.Length; i++)
        {
            if (i != 0)
            {
                _cameras[i].gameObject.SetActive(false);
            }
        }
        _cameraSelector = FindObjectOfType<CameraSelector>();
    }

    // Update is called once per frame
    void Update()
    {
        // split
        if (Input.GetKeyDown(KeyCode.K))
        {
            if ((_numberOfActiveStates < _maxNumberOfStates) && !_cameraSelector.isScreenSelectorEnabled)
            {
                foreach (ThirdPersonCamera camera in _cameras)
                {
                    if (!_cameraSelector.IsActive(camera))
                    {
                        camera.target.gameObject.SetActive(true);
                        camera.target.position = _cameraSelector.lastActiveCamera.target.position;
                        camera.transform.position = new Vector3(camera.target.position.x, camera.target.position.y, camera.target.position.z - camera.distance);
                        _cameraSelector.AddCamera(camera);
                        ++_numberOfActiveStates;
                        _cameraSelector.EnableSelector();
                        break;
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if ((_numberOfActiveStates > 1) && !_cameraSelector.isScreenSelectorEnabled)
            {
                PlayerController[] playerControllers = FindObjectsOfType<PlayerController>();
                foreach (PlayerController playerController in playerControllers)
                {
                    foreach (PlayerController playerController2 in playerControllers)
                    {
                        if (playerController.roomId != playerController2.roomId)
                        {
                            return;
                        }
                    }
                }
                _numberOfActiveStates = 1;
                _cameraSelector.RemoveDisabledCameras();
            }
        }
    }
}
