using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCarScript : MonoBehaviour
{
    public Rigidbody sphere;
    public Transform kartModel;
    float speed, currentSpeed;
    float rotate, currentRotate;
    public float gravity = 10f;
    public float steering = 80f;
    public float acceleration = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sphere.transform.position - new Vector3(0, .2f, 0);
        if (Input.GetButton("Fire1"))
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
    void FixedUpdate()
    {
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);
        sphere.AddForce(kartModel.transform.forward * currentSpeed, ForceMode.Acceleration);
        sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
    }
    public void Steer(int direction, float amount)
    {
        rotate = (steering * direction) * amount;
    }
}
