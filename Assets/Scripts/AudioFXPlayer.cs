using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXPlayer : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip openDoor;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
}
