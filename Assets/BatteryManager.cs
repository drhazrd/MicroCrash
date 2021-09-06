using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryManager : MonoBehaviour
{
    [SerializeField]
    float decayRate = 6f;
    public float maxbatteryLife = 1000f, currbatteryLife;
    public bool batteryDead = false;
    public bool batterDecayActive;
    void Start()
    {
        currbatteryLife = maxbatteryLife;
        UIManager.instance_UI.SetMaxValue(maxbatteryLife, currbatteryLife);

    }

    void Update()
    {
        float decayMultiplier = this.gameObject.GetComponent<RCController>().decayMultiplier;
        UIManager.instance_UI.batteryHP = currbatteryLife;
        if (currbatteryLife > 0 && batterDecayActive && !batteryDead) { currbatteryLife -= decayRate * decayMultiplier*Time.deltaTime; }
        else if (currbatteryLife <= 0.01f)
        {
                currbatteryLife = 0;
            if (currbatteryLife == 0&&!batteryDead)
            {
                batteryDead = true;
                BatteryDrained();
            }
            if (batteryDead)
            {
                batterDecayActive = false;
            }
        }
    }

    private void BatteryDrained()
    {
        Debug.Log("Still");
        LevelManager.lv_instance.StartCoroutine("GameOver");
        LevelManager.lv_instance.MovementFreeze();
        return;
    }
}
