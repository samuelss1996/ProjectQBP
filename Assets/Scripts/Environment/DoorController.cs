using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.0f;
    [SerializeField]
    private int numberOfTriggers = 1;

    private Vector3 _basePosition = Vector3.zero;
    private Rigidbody _rigidbody = null;
    private float _height = 0.0f;
    private int _numberOfTriggersActivated = 0;
    
    public int numberOfTriggersActivated
    {
        set { _numberOfTriggersActivated = value; }
        get { return _numberOfTriggersActivated;  }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _basePosition = transform.position;
        Collider collider = GetComponent<Collider>();
        if (collider)
        {
            _height = collider.bounds.size.y;
        }
    }
    
    private void FixedUpdate()
    {
        if ((_numberOfTriggersActivated == numberOfTriggers) && (_rigidbody.position.y < _height))
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector3.up * movementSpeed * Time.fixedDeltaTime);

        }
        else if ((_numberOfTriggersActivated < numberOfTriggers) && (_rigidbody.position.y > _basePosition.y))
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector3.down * movementSpeed * Time.fixedDeltaTime);
        }
    }
}
