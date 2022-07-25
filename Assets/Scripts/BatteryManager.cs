using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField]
    float decayRate = 4f;
    public float maxbatteryLife = 1000f, currbatteryLife;
    public bool batteryDead = false;
    public bool batterDecayActive;
    public bool infinityBattery;

    void Start()
    {
        currbatteryLife = maxbatteryLife;
        UIManager.instance_UI.SetMaxValue(maxbatteryLife, currbatteryLife);

    }

    void Update()
    {
        float decayMultiplier = this.gameObject.GetComponent<CarPlayerController>().decayMultiplier;
        UIManager.instance_UI.batteryHP = currbatteryLife;
        if (!infinityBattery) {
            if (currbatteryLife > 0 && batterDecayActive && !batteryDead) { currbatteryLife -= decayRate * decayMultiplier * Time.deltaTime; }
            else if (currbatteryLife <= 0.01f)
            {
                currbatteryLife = 0;
                if (currbatteryLife == 0 && !batteryDead)
                {
                    batteryDead = true;
                    BatteryDrained();
                }
                if (batteryDead)
                {
                    batterDecayActive = false;
                }
            } }
    }

    private void BatteryDrained()
    {
        Debug.Log("Still");
        LevelManager.lv_instance.batteryOut = true;
        LevelManager.lv_instance.MovementFreeze();
        return;
    }
}
