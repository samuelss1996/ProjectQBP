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

    private void Start()
    {
        _doorController = GetComponentInParent<DoorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody && (weightThreshold <= rigidbody.mass))
        {
            _doorController.numberOfTriggersActivated = _doorController.numberOfTriggersActivated + 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody && (!_oneTimePress || (_oneTimePress && (_doorController.numberOfTriggersActivated < _doorController.numOfTriggers))))
        {
            _doorController.numberOfTriggersActivated = _doorController.numberOfTriggersActivated - 1;
        }
    }
}
