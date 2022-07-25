using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    public int SFXid;
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.audio_instance.PlaySFX(SFXid); 
    }
}
