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
    public SpringJoint m_balanceSpringF; //Forward balance spring
    public SpringJoint m_balanceSpringR; //Rear balance spring

    public float maxMotorTorque;
    public float maxSteeringAngle;

    public float m_maxFuel;
    public float m_fuel;
    public float m_boostFuelDrainRate;
    public float m_jumpFuelDrain;

    public float m_maxJumpCooldown;
    public float m_jumpCooldown;
    

    public Vector3 m_jumpForce;
    public Vector3 m_boostForce;

    /// <summary>
    /// Runs at startup
    /// </summary>
    public void Start()
    {
        m_rigidBody.centerOfMass = new Vector3(0, -0.15f, 0);
    }


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
        if(Input.GetKeyDown(KeyCode.Space) && m_fuel >= m_jumpFuelDrain-5 && m_jumpCooldown <= 0)
        {
            m_fuel -= m_jumpFuelDrain;
            m_jumpCooldown = m_maxJumpCooldown;
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
            m_rigidBody.AddForce(m_jumpForce.y * transform.up, ForceMode.Impulse);
        }
        else if(m_jumpCooldown > 0)
        {
            m_jumpCooldown -= Time.deltaTime;
        }

        //Update balance spring positions
        m_balanceSpringF.connectedAnchor = transform.position + m_balanceSpringF.anchor;
        m_balanceSpringR.connectedAnchor = transform.position + m_balanceSpringR.anchor;

        //Handle reset
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TESTRESET();
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
            m_rigidBody.AddForce(m_boostForce.z * transform.forward, ForceMode.Force);
            m_fuel -= m_boostFuelDrainRate * Time.deltaTime;
        }
    }

    /// <summary>
    /// Simple test function that resets the object
    /// </summary>
    public void TESTRESET()
    {
        m_fuel = m_maxFuel;
        m_rigidBody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}