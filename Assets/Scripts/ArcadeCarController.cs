//**************************************************************************************
// File: ArcadeCarController.cs
//
// Purpose: 
//
// Written By: Salvatore Hanusiewicz
//**************************************************************************************

using UnityEngine;
using System.Collections;

public class ArcadeCarController : MonoBehaviour
{
    #region Declarations
    public Rigidbody m_rigidBody;
    public Transform m_centerOfMass;

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

    public float m_jumpForce;
    public float m_boostForce;
    public float m_strafeForce;

    public float m_initialBoostForce;
    public float m_initialStrafeForce;
    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    private void Start()
    {

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        //Handle jump
        if (Input.GetKeyDown(KeyCode.Space) && m_fuel >= m_jumpFuelDrain - 5 && m_jumpCooldown <= 0)
        {
            m_fuel -= m_jumpFuelDrain;
            m_jumpCooldown = m_maxJumpCooldown;
            m_rigidBody.velocity = new Vector3(m_rigidBody.velocity.x, 0, m_rigidBody.velocity.z);
            m_rigidBody.AddForce(m_jumpForce * transform.up, ForceMode.Impulse);
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
            m_rigidBody.AddForce(m_boostForce * transform.forward, ForceMode.Impulse);
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

        //Handle reset
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            TESTRESET();
        }
    }

    /// <summary>
    /// Update is called once per physics tick
    /// </summary>
    private void FixedUpdate()
    {
        //If the player is holding the boost button, apply the boost force and drain fuel
        if (Input.GetKey(KeyCode.W) && m_fuel > 0)
        {
            m_rigidBody.AddForce(m_boostForce * transform.forward, ForceMode.Force);
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
    }

    #region Properties
    #endregion
}