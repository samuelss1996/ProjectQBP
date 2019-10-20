using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuantumController : MonoBehaviour
{
    [SerializeField]
    private int _maxNumberOfStates = 2;
    [SerializeField]
    private ThirdPersonCamera[] _cameras;

    [SerializeField]
    private ThirdPersonCamera main;
    [SerializeField]
    private ThirdPersonCamera big;
    [SerializeField]
    private ThirdPersonCamera small;

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
                ++_numberOfActiveStates;
                Vector3 position = main.target.position;
                main.target.gameObject.SetActive(false);
                main.gameObject.SetActive(false);
                _cameraSelector.RemoveCamera(main);

                big.target.position = position;
                big.target.gameObject.SetActive(true);
                _cameraSelector.AddCamera(big);
                //big.gameObject.SetActive(true);
                small.target.position = position;
                small.target.gameObject.SetActive(true);
                _cameraSelector.AddCamera(small);
                //small.gameObject.SetActive(true);
                _cameraSelector.EnableSelector();

                //foreach (ThirdPersonCamera camera in _cameras)
                //{
                //    if (!_cameraSelector.IsActive(camera))
                //    {
                //        GameObject[] states = GameObject.FindGameObjectsWithTag("BigPlayer");
                //        camera.GetComponent<Camera>().cullingMask &= ~(1 << camera.target.gameObject.layer);
                //        camera.target = states[0].transform;
                //        camera.GetComponent<Camera>().cullingMask |= 1 << camera.target.gameObject.layer;
                //        camera.target.gameObject.GetComponent<MeshRenderer>().enabled = true;
                //        //camera.target.gameObject.GetComponent<Collider>().enabled = true;
                //        camera.target.position = _cameraSelector.lastActiveCamera.target.position;
                //        camera.transform.position = new Vector3(camera.target.position.x, camera.target.position.y, camera.target.position.z - camera.distance);
                //        _cameraSelector.AddCamera(camera);
                //        ++_numberOfActiveStates;
                //        _cameraSelector.EnableSelector();
                //        break;
                //    }
                //}
                //GameObject[] states2 = GameObject.FindGameObjectsWithTag("SmallPlayer");
                //states2[0].transform.position = _cameraSelector.lastActiveCamera.target.position;
                //_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<MeshRenderer>().enabled = false;
                ////_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<Collider>().enabled = false;
                //_cameraSelector.lastActiveCamera.GetComponent<Camera>().cullingMask &= ~(1 << _cameraSelector.lastActiveCamera.target.gameObject.layer);
                //_cameraSelector.lastActiveCamera.target = states2[0].transform;
                //_cameraSelector.lastActiveCamera.GetComponent<Camera>().cullingMask |= 1 << _cameraSelector.lastActiveCamera.target.gameObject.layer;
                //_cameraSelector.lastActiveCamera.gameObject.layer = _cameraSelector.lastActiveCamera.target.gameObject.layer;
                //_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<MeshRenderer>().enabled = true;
                //_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<Collider>().enabled = true;
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
                main.target.gameObject.SetActive(true);
                main.gameObject.SetActive(true);
                main.target.transform.position = new Vector3(_cameraSelector.lastActiveCamera.target.position.x, main.target.transform.position.y, _cameraSelector.lastActiveCamera.target.position.z);

                small.gameObject.SetActive(false);
                small.target.gameObject.SetActive(false);
                big.gameObject.SetActive(false);
                big.target.gameObject.SetActive(false);

                //GameObject[] states = GameObject.FindGameObjectsWithTag("BigPlayer");
                //states[0].gameObject.GetComponent<MeshRenderer>().enabled = false;
                //states = GameObject.FindGameObjectsWithTag("SmallPlayer");
                //states[0].gameObject.GetComponent<MeshRenderer>().enabled = false;

                //states = GameObject.FindGameObjectsWithTag("MainPlayer");
                //states[0].transform.position = _cameraSelector.lastActiveCamera.target.position;
                //_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<MeshRenderer>().enabled = false;
                ////_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<Collider>().enabled = false;
                //_cameraSelector.lastActiveCamera.GetComponent<Camera>().cullingMask &= ~(1 << _cameraSelector.lastActiveCamera.target.gameObject.layer);
                //_cameraSelector.lastActiveCamera.target = states[0].transform;
                //_cameraSelector.lastActiveCamera.GetComponent<Camera>().cullingMask |= 1 << _cameraSelector.lastActiveCamera.target.gameObject.layer;
                //_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<MeshRenderer>().enabled = true;
                //_cameraSelector.lastActiveCamera.target.gameObject.GetComponent<Collider>().enabled = true;

                _cameraSelector.RemoveDisabledCameras();
            }
        }
    }
}
