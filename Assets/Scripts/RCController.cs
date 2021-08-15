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
    public float forwardAccel = 8f;
    float reverseAccel = 4f;
    float maxSpeed = 50f;
    float turnStrength = 180;
    float gravityForce = 10f;
    public float steering = 80f;
    public float acceleration = 30f;
    public float accelerationDampner = 0.1f;
    public float gravity = 10f;
    //public Collider playerScoreTrigger;
    //public ScoreKeeper sKeeper;

    [Header("Model Parts")]
    public Transform kartBody;
    public Transform frontRWheels, frontLWheels, backRWheels, backLWheels;
    private float dragOnGround = 3f;
    private float maxWheelTurn = 35f;

    void Start()
    {
        theRB.transform.parent = null;
    }

    void Update()
    {
        speedInput = 0f;
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
        if (grounded)
        {
            theRB.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                theRB.AddForce(transform.forward * speedInput);
            }
        }else
        {
            theRB.drag = 0.1f;
            theRB.AddForce(Vector3.up * -gravityForce * 100f);
        }
    }

    public void Steer(int direction, float amount)
    { }
    public void Steer(int direction, float amount, int number)
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 2));
    }
}

