using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zRCController : MonoBehaviour
{
    public Rigidbody sphere;
    public Transform kartModel;
    float speed, currentSpeed;
    float rotate, currentRotate;
    public float gravity = 10f;
    public float steering = 80f;
    public float acceleration = 30f;
    public LayerMask layerMask;

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
        if (Input.GetAxis("Horizontal") != 0)
        {
            int dir = Input.GetAxis("Horizontal") > 0 ? 1 : -1;
            float amount = Mathf.Abs((Input.GetAxis("Horizontal")));
            Steer(dir, amount);
        }
        currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f); speed = 0f;
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f); rotate = 0f;
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

        kartModel.up = Vector3.Lerp(kartModel.up, hitNear.normal, Time.deltaTime * 8.0f);
        kartModel.Rotate(0, transform.eulerAngles.y, 0);
    }



    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 2));
    }
}

