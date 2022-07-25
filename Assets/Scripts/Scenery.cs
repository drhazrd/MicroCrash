using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenery : MonoBehaviour
{
    public AudioSource soundID;
    // Start is called before the first frame update
    private void Awake()
    {
        soundID = GetComponent<AudioSource>();
        
    }
    void Start()
    {
        soundID.volume = 0.1f;
        soundID.pitch = 1.65f;
    }

    public void OnCollisionEnter()
    {
        soundID.Play();
    }
}
