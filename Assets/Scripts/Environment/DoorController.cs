using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 2.0f;
    [SerializeField]
    private int numberOfTriggers = 1;

    private AudioFXPlayer audioFxPlayer;
    private Vector3 _basePosition = Vector3.zero;
    private Rigidbody _rigidbody = null;
    private float _height = 0.0f;
    private int _numberOfTriggersActivated = 0;

    private bool soundPlayed;

    public int numOfTriggers
    {
        get { return numberOfTriggers; }
    }

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

        audioFxPlayer = GameObject.FindWithTag("AudioManager").GetComponent<AudioFXPlayer>();
    }
    
    private void FixedUpdate()
    {
        if ((_numberOfTriggersActivated == numberOfTriggers) && (_rigidbody.position.y > -_height))
        {
            _rigidbody.MovePosition(_rigidbody.position + Vector3.up * movementSpeed * Time.fixedDeltaTime);
            PlayDoorOpenOnlyOnce();
        }
        else if ((_numberOfTriggersActivated < numberOfTriggers) && (_rigidbody.position.y < _basePosition.y))
        {
            //_rigidbody.MovePosition(_rigidbody.position + Vector3.down * movementSpeed * Time.fixedDeltaTime);
            _rigidbody.position = _basePosition;
            soundPlayed = false;
        }
    }

    private void PlayDoorOpenOnlyOnce()
    {
        if(!soundPlayed)
        {
            audioFxPlayer.audioSource.PlayOneShot(audioFxPlayer.openDoor);
        }

        soundPlayed = true;
    }
}
