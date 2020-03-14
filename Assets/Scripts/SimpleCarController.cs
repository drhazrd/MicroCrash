using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public Rigidbody m_rigidBody;

    public float maxMotorTorque;
    public float maxSteeringAngle;
    public float m_maxFuel;
    public float m_fuel;
    public float m_fuelDrainRate;
    public Vector3 m_jumpForce;
    public Vector3 m_boostForce;
    

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void Update()
    {
        //Handle jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_rigidBody.AddForce(m_jumpForce, ForceMode.Impulse);
        }
    }


    public void FixedUpdate()
    {
        //TO DO:
        //Handle boost
        //Handle breaking

        //Determine motor power
        float motor = maxMotorTorque * Input.GetAxis("Vertical");

        //Determine steering angle
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //Loop through all the axles
        foreach (AxleInfo axleInfo in axleInfos)
        {
            //If the current axles handles steering, apply the steering to it
            //TO DO: Extend this functionality to have 3 modes: Off, Normal, Inverted (this allows rear wheels to steer in the opposite direction of the front wheels)
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            //If the current axles has power, apply the torque to it
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            //Apply the current position and rotation of the wheel collidier to the first child object under each wheel
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        //If the player is holding the boost button, apply the boost force and drain fuel
        if(Input.GetKey(KeyCode.W) && m_fuel > 0)
        {
            m_rigidBody.AddForce(m_boostForce, ForceMode.Force);
            m_fuel -= m_fuelDrainRate * Time.deltaTime;
        }
    }
}