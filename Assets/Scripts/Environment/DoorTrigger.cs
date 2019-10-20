using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private bool _oneTimePress = true;
    [SerializeField]
    private float weightThreshold = 0.6f;

    private DoorController _doorController = null;
    private Rigidbody _collidingRb = null;

    private void Start()
    {
        _doorController = GetComponentInParent<DoorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            playerController.onTrigger = true;
        }

        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody && (weightThreshold <= rigidbody.mass))
        {
            _doorController.numberOfTriggersActivated = _doorController.numberOfTriggersActivated + 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();
        if (playerController)
        {
            playerController.onTrigger = false;
        }

        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody && (weightThreshold <= rigidbody.mass) && (!_oneTimePress || (_oneTimePress && (_doorController.numberOfTriggersActivated < _doorController.numOfTriggers))))
        {
            _doorController.numberOfTriggersActivated = _doorController.numberOfTriggersActivated - 1;
        }
    }
}
