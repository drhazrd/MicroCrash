using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager:MonoBehaviour
{
    public static LevelManager lv_instance { get; internal set; }
    public bool movementFreeze = true;

    private void Awake()
    {
        if (lv_instance == null)
        {
            lv_instance = this;
        }
    }
}