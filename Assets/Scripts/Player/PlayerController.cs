using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _movementSpeed = 1.0f;

    private Vector3 _input;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        _input = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        _rigidbody.MovePosition(_rigidbody.position + _input * _movementSpeed * Time.fixedDeltaTime);
    }
}
