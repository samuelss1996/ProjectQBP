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
    private bool _isUsed = false;
    private Rigidbody _collidingRb = null;

    private void Start()
    {
        _doorController = GetComponentInParent<DoorController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (!_isUsed && rigidbody && (weightThreshold <= rigidbody.mass))
        {
            _collidingRb = rigidbody;
            _doorController.numberOfTriggersActivated = _doorController.numberOfTriggersActivated + 1;
            _isUsed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        if (rigidbody && (rigidbody == _collidingRb) && (!_oneTimePress || (_oneTimePress && (_doorController.numberOfTriggersActivated < _doorController.numOfTriggers))))
        {
            _doorController.numberOfTriggersActivated = _doorController.numberOfTriggersActivated - 1;
            _collidingRb = null;
            _isUsed = false;
        }
    }
}
