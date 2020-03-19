using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float maxSteer;
    public float maxSpeed = 250f;
    public float nO2Multiplier = 1;

    public WheelCollider[] fWheel, rWheel;

    void FixedUpdate()
    {
        float motorT = Input.GetAxis("Vertical") * nO2Multiplier * maxSpeed;
        float steerA = Input.GetAxis("Horizontal") * nO2Multiplier * maxSteer;

        rWheel[0].motorTorque = motorT;
        rWheel[1].motorTorque = motorT;

        fWheel[0].steerAngle = steerA;
        fWheel[1].steerAngle = steerA;

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
            rWheel[0].brakeTorque = 0;
            rWheel[1].brakeTorque = 0;
        }
        else
        {
            rWheel[0].brakeTorque = 220;
            rWheel[1].brakeTorque = 220;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            nO2Multiplier = 5f;
        }
        else
        {
            nO2Multiplier = 1f;
        }
    }
}
