using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject target;
    private float _offset;

    void Start()
    {
        _offset = transform.position.z - target.transform.position.z;
        Cursor.visible = false;
    }
    
    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z + _offset);
    }
}
