using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.audio_instance.PlayVictory();
    }
}
