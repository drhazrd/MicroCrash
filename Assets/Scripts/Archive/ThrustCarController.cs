using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrustCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public Rigidbody m_rigidBody;
    public Transform m_centerOfMass;
    public SpringJoint m_balanceSpringFront; //Front balance spring
    public SpringJoint m_balanceSpringRear; //Rear balance spring
    public SpringJoint m_balanceSpringLeft; //Left balance spring
    public SpringJoint m_balanceSpringRight; //Right balance spring

    public float maxMotorTorque;
    public float m_maxBrakeTorque;
    public float maxSteeringAngle;

    public float m_maxFuel;
    public float m_fuel;
    public float m_boostFuelDrainRate;
    public float m_jumpFuelDrain;

    public float m_maxJumpCooldown;
    public float m_jumpCooldown;

    public float m_maxBoostCooldown;
    public float m_boostCooldown;

    public float m_maxStrafeBoostCooldown;
    public float m_strafeBoostCooldown;

    public Vector3 m_jumpForce;
    public Vector3 m_boostForce;
    public float m_strafeForce;

    /// <summary>
    /// Runs at startup
    /// </summary>
    public void Start()
    {
        if (m_centerOfMass != null)
        {
            m_rigidBody.centerOfMass = transform.InverseTransformPoint(m_centerOfMass.position);
        }
        else
        {
            m_rigidBody.centerOfMass = Vector3.zero; //new Vector3(0, -0.15f, 0);
        }

        UpdateBalanceSprings();
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
        if (Input.GetKeyDown(KeyCode.Space) && m_fuel >= m_jumpFuelDrain - 5 && m_jumpCooldown <= 0)
        {
            m_fuel -= m_jumpFuelDrain;
            m_jumpCooldown = m_maxJumpCooldown;
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
            m_rigidBody.AddForce(m_jumpForce.y * transform.up, ForceMode.Impulse);
        }
        else if (m_jumpCooldown > 0)
        {
            m_jumpCooldown -= Time.deltaTime;
        }

        //Handle thrust initial boost
        //If the player presses the boost key, give them an initial boost and put this on cooldown
        if (Input.GetKeyDown(KeyCode.W) && m_fuel > 0 && m_boostCooldown <= 0)
        {
            m_boostCooldown = m_maxBoostCooldown;
            m_rigidBody.AddForce(m_boostForce.z * transform.forward, ForceMode.Impulse);
            m_fuel -= m_boostFuelDrainRate;
        }
        else if (m_boostCooldown > 0)
        {
            m_boostCooldown -= Time.deltaTime;
        }

        //If the player presses a strafe key, give them an initial boost and put this on cooldown
        if (Input.GetKeyDown(KeyCode.A) && m_strafeBoostCooldown <= 0)
        {
            m_strafeBoostCooldown = m_maxStrafeBoostCooldown;
            m_rigidBody.AddForce(m_strafeForce * -transform.right, ForceMode.Impulse);
        }
        else if (Input.GetKeyDown(KeyCode.D) && m_strafeBoostCooldown <= 0)
        {
            m_strafeBoostCooldown = m_maxStrafeBoostCooldown;
            m_rigidBody.AddForce(m_strafeForce * transform.right, ForceMode.Impulse);
        }
        else if (m_strafeBoostCooldown > 0)
        {
            m_strafeBoostCooldown -= Time.deltaTime;
        }

        UpdateBalanceSprings();

        //Handle reset
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TESTRESET();
        }
    }


    public void FixedUpdate()
    {
        //TO DO:
        //Handle breaking

        //Determine motor power
        float motor = maxMotorTorque; // * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.S))
        {
            motor = maxMotorTorque * 0f;
            //brakeTorque = m_maxBrakeTorque;
        }

        //Determine steering angle
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

        //Loop through all the axles
        foreach (AxleInfo axleInfo in axleInfos)
        {
            //If the current axle handles steering normally, apply it
            if (axleInfo.steering == 1)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            //If the current axle uses inverted steering, apply the negative of the current steering angle
            else if (axleInfo.steering == 2)
            {
                axleInfo.leftWheel.steerAngle = -steering;
                axleInfo.rightWheel.steerAngle = -steering;
            }

            //Apply the current position and rotation of the wheel collidier to the first child object under each wheel
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }

        //If the player is holding the boost button, apply the boost force and drain fuel
        if (Input.GetKey(KeyCode.W) && m_fuel > 0)
        {
            m_rigidBody.AddForce(m_boostForce.z * transform.forward, ForceMode.Force);
            m_fuel -= m_boostFuelDrainRate * Time.deltaTime;
        }

        //If the player is holding left or right, apply some thrust to enable air control and more stable ground control.. hopefully lol
        if (Input.GetKey(KeyCode.A))
        {
            m_rigidBody.AddForce(m_strafeForce * -transform.right, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_rigidBody.AddForce(m_strafeForce * transform.right, ForceMode.Force);
        }
    }

    /// <summary>
    /// Update balance spring positions
    /// </summary>
    public void UpdateBalanceSprings()
    {
        //Front and back balance springs
        Vector3 springForward = Vector3.Normalize(new Vector3(transform.forward.x, 0, transform.forward.z));
        m_balanceSpringFront.connectedAnchor = transform.position + (m_balanceSpringFront.anchor.z * springForward);
        m_balanceSpringRear.connectedAnchor = transform.position + (m_balanceSpringRear.anchor.z * springForward);

        //Left and right balance springs
        Vector3 springRight = Vector3.Normalize(new Vector3(transform.right.x, 0, transform.right.z));
        m_balanceSpringLeft.connectedAnchor = transform.position + (m_balanceSpringLeft.anchor.z * springRight);
        m_balanceSpringRight.connectedAnchor = transform.position + (m_balanceSpringRight.anchor.z * springRight);
    }

    /// <summary>
    /// Simple test function that resets the object
    /// </summary>
    public void TESTRESET()
    {
        m_maxFuel = 100000;
        m_boostFuelDrainRate = 0;
        m_jumpFuelDrain = 0;
        m_maxJumpCooldown = 0;

        m_fuel = m_maxFuel;
        m_rigidBody.velocity = Vector3.zero;
        m_rigidBody.angularVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.Euler(Vector3.zero);

        UpdateBalanceSprings();
    }
}
