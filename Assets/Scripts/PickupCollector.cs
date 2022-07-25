using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollector : MonoBehaviour
{
    public Transform player;
    public BatteryManager playerBattery;

    void Awake()
    {
        player = this.transform.parent.gameObject.GetComponentInChildren<BatteryManager>().transform;
        playerBattery = player.GetComponentInChildren<BatteryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
