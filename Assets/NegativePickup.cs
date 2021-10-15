using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class NegativePickup : MonoBehaviour
{
    bool collected = false;
    float value = -85;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KartBody")
        {
            AudioManager.audio_instance.PlaySFX(1);
            collected = true;
            Debug.Log("PickupCollector collected!");
            other.GetComponent<PickupCollector>().playerBattery.currbatteryLife += value;
            Collected();
        }
    }
    public void Collected()
    {
        Destroy(gameObject);
    }
}
