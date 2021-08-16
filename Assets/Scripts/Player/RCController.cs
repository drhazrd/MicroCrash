using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCController : MonoBehaviour
{
    public Rigidbody theRB;
    float speedInput, turnInput;
    bool grounded;
    public LayerMask whatIsGround;
    float groundRayLength = .5f;
    public Transform groundRayPoint;

    [Header("Parameters")]
    float forwardAccel = 80f;
    float reverseAccel = 35f;
    float turnStrength = 180;
    float gravityForce = 10f;
    UIManager UIKeeper;

    [Header("Model Parts")]
    public Transform kartBody;
    public Transform frontRWheels, frontLWheels, backRWheels, backLWheels;
    private float dragOnGround = 3f;
    private float maxWheelTurn = 35f;

    [Header("Vehicle FX")]
    public ParticleSystem[] dustTrails;
    public GameObject thrusterFX;
    public float maxEmission = 25f;
    private float emissionRate;
    private int boostMultiplier = 1;
    private float boostlength = 1;


    void Start()
    {
        theRB.transform.parent = null;
        UIKeeper = UIManager.instance_UI;
        thrusterFX.SetActive(false);
    }

    void Update()
    {
        speedInput = 0f;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Boost");
            Debug.Log("Boost Triggered");
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;
        } else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }
        turnInput = Input.GetAxis("Horizontal");
        if (grounded) { 
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
        }

        frontLWheels.localRotation = Quaternion.Euler(frontLWheels.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, frontLWheels.localRotation.eulerAngles.z);
        frontRWheels.localRotation = Quaternion.Euler(frontRWheels.localRotation.eulerAngles.x, turnInput * maxWheelTurn, frontRWheels.localRotation.eulerAngles.z);
        transform.position = theRB.transform.position;
    }
    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if(Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }

        emissionRate = 0;

        if (grounded)
        {
            theRB.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                theRB.AddForce(transform.forward * speedInput * boostMultiplier);

                emissionRate = maxEmission;
            }
        }else
        {
            theRB.drag = 0.1f;
            theRB.AddForce(Vector3.up * -gravityForce * 100f);
                emissionRate = 0f;
        }
        foreach(ParticleSystem part in dustTrails)
        {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = emissionRate;
        }
    }

    public IEnumerator Boost()
    {
        Debug.Log("Boost Activated");
        boostMultiplier = 5;
        thrusterFX.SetActive(true);
        yield return new WaitForSeconds(boostlength);
        thrusterFX.SetActive(false);
        boostMultiplier = 1;
    }
    public void OnTriggerEnter(Collider other)
    {
        UIKeeper.AddScore(boostMultiplier);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 2));
    }
}

