using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashActivator : MonoBehaviour
{
    public Rigidbody[] crashObjects;
    // Start is called before the first frame update
    void Start()
    {
        //crashObjects = new List<Rigidbody>();
        crashObjects = GetComponentsInChildren<Rigidbody>();
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void ActivateCrash()
    {
        foreach (Rigidbody crashobjs in crashObjects)
        {
            crashobjs.GetComponentInChildren<BoxCollider>().enabled = true;
            crashobjs.useGravity = true;
        }
    }
    private void Reset()
    {
        foreach (Rigidbody resetobject in crashObjects)
        {
            resetobject.useGravity = false;
            resetobject.GetComponentInChildren<BoxCollider>().enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "KartBody")
        {
            ActivateCrash();
        }
    }
}
