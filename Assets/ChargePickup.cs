using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Collider))]
public class ChargePickup : MonoBehaviour
{
    bool collected = false;
    float value = 350, coolDown;
    public GameObject item;
    public Image itemMinimapImage;

    private void Start()
    {
        item = transform.GetChild(0).gameObject;
        itemMinimapImage = transform.GetChild(1).gameObject.GetComponent<Image>();
    }
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
         StartCoroutine(Collection(coolDown));
    }
    public IEnumerator Collection(float respawnTimer)
    {
        yield return new WaitForSeconds(respawnTimer);
    }
}
