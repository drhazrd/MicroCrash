using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCarScript : MonoBehaviour
{
    public Rigidbody sphere;
    public Transform kartModel;
    public Transform kartNormal;
    float speed, currentSpeed;
    float rotate, currentRotate;

    [Header("Parameters")]
    public float steering = 80f;
    public float acceleration = 30f;
    public float accelerationDampner = 0.1f;
    public float gravity = 10f;
    public LayerMask layerMask;
    public Collider playerScoreTrigger;
    public ScoreKeeper sKeeper;


    [Header("Model Parts")]
    public Transform frontRWheels;
    public Transform frontLWheels;
    public Transform backRWheels;
    public Transform backLWheels;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = sphere.transform.position - new Vector3(0, .2f, 0);

        if (Input.GetAxis("Vertical") != 0)
        {
            speed = acceleration;
        }
        else if (Input.GetAxis("Vertical") <= 0)
        {
            Physics.sleepThreshold = accelerationDampner;
            speed = 0;

        }
        
        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
        }
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;

        frontRWheels.localEulerAngles = new Vector3(0, (Input.GetAxis("Horizontal") * steering), frontRWheels.localEulerAngles.z);
        frontLWheels.localEulerAngles = new Vector3(0, (Input.GetAxis("Horizontal") * steering), frontLWheels.localEulerAngles.z);
        frontRWheels.localEulerAngles += new Vector3(sphere.velocity.magnitude / 4, 0, 0);
        frontLWheels.localEulerAngles += new Vector3(sphere.velocity.magnitude / 4, 0, 0);
        backRWheels.localEulerAngles += new Vector3(sphere.velocity.magnitude / 4, 0, 0);
        backLWheels.localEulerAngles += new Vector3(sphere.velocity.magnitude / 4, 0, 0);
    }
    private void FixedUpdate()
    {
        sphere.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);
        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);

        RaycastHit hitOn;
        RaycastHit hitNear;

        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitOn, 1.1f, layerMask);
        Physics.Raycast(transform.position + (transform.up * .1f), Vector3.down, out hitNear, 2.0f, layerMask);

        //Normal Rotation
        kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartNormal.Rotate(0, transform.eulerAngles.y, 0);
    }
    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ScoreTarget")
        {
            sKeeper.score += 10;
        }
    }
}
