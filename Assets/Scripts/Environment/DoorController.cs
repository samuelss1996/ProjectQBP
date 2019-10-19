using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.0f;

    private Vector3 _basePosition = Vector3.zero;
    private bool _open = false;
    private Rigidbody _rigidbody = null;
    private float _height = 0.0f;
    
    public bool open
    {
        set { _open = value; }
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
        if (_open && (_rigidbody.position.y < _height))
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector3.up * movementSpeed * Time.fixedDeltaTime);
        }
        else if(_rigidbody.position.y > _basePosition.y)
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector3.down * movementSpeed * Time.fixedDeltaTime);
        }
    }
}
