using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTrigger : MonoBehaviour
{
    public int sfxID;
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.audio_instance.PlaySFX(sfxID);
    }
}
