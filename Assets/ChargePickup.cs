using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class ChargePickup : MonoBehaviour
{
    bool collected = false;
    public float value = 350;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "KartBody")
        {
            collected = true;
            AudioManager.audio_instance.PlaySFX(0);
            other.GetComponent<PickupCollector>().playerBattery.currbatteryLife += value;
            Collected();
        }
    }
    public void Collected()
    {
        Destroy(gameObject);
    }
}
