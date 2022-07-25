using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class CarPlayerController : MonoBehaviour
{
    public int playerNumber;
    public Rigidbody driveCollider;
    public LayerMask whatIsGround;
    public Transform groundRayPoint;
    float speedInput, turnInput;
    bool grounded;
    float groundRayLength = .5f;
    float snapSpeed = 4.25f;

    [Header("Parameters")]
    float forwardAccel = 80f;
    float reverseAccel = 35f;
    float turnStrength = 120;
    float gravityForce = 10f;
    private float boostMultiplier = 1;
    private float boostlength = 1;
    bool isDrifting;
    private int driftDirection;
    float driftMultiplier = 1f;

    [Header("Model Parts")]
    public Transform kartBody;
    public Transform frontRWheels, frontLWheels, backRWheels, backLWheels;
    private float dragOnGround = 3f;
    private float maxWheelTurn = 35f;

    [Header("Player Data")]
    public int playerExperience;
    public int playerLevel;
    public int playerCurrency;


    [Header("Battery Data")]
    BatteryManager battery;
    public bool batteryLife;
    float batteryDecayrate;
    public float decayMultiplier;
    bool batteryActive;

    [Header("Vehicle FX")]
    public GameObject sFX;
    public ParticleSystem[] dustTrails;
    public GameObject thrusterFX;
    public float maxEmission = 25f;
    private float emissionRate;

    void Start()
    {
        driveCollider.transform.parent = null;
        thrusterFX.SetActive(false);
        battery = GetComponent<BatteryManager>();
    }

    void Update()
    {
        decayMultiplier = boostMultiplier;
        speedInput = 0f;
        batteryActive = Input.GetAxis("Acceleration") != 0;
        battery.batterDecayActive = batteryActive;
        AudioManager.audio_instance.soundEffect[3].gameObject.SetActive(batteryActive);

        if (GameManager.gm_instance.movementAllowed)
        {
            if (Input.GetButtonDown("Drift") && !isDrifting && Input.GetAxis("Horizontal") != 0)
            {
                Drift();
            }
            if (Input.GetButtonUp("Drift") && isDrifting)
            {
                isDrifting = false;
            }
            if (Input.GetButton("Boost"))
            {
                StartCoroutine("Boost");
                Debug.Log("Boost Triggered");
            }
            if (Input.GetAxis("Acceleration") > 0)
            {
                speedInput = Input.GetAxis("Acceleration") * forwardAccel * 1000f;
            }
            else if (Input.GetAxis("Acceleration") < 0)
            {
                speedInput = Input.GetAxis("Acceleration") * reverseAccel * 1000f;
            }
            turnInput = Input.GetAxis("Horizontal");
        }
        if (grounded)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * (turnStrength * driftMultiplier) * Time.deltaTime * Input.GetAxis("Acceleration"), 0f));
            if (isDrifting)
            {
                driftMultiplier = 1.5f;
            }
            else if (!isDrifting)
            {
                driftMultiplier = 1f;
            }

        }
        frontLWheels.localRotation = Quaternion.Euler(frontLWheels.localRotation.eulerAngles.x, turnInput * maxWheelTurn, frontLWheels.localRotation.eulerAngles.z);
        frontRWheels.localRotation = Quaternion.Euler(frontRWheels.localRotation.eulerAngles.x, turnInput * maxWheelTurn, frontRWheels.localRotation.eulerAngles.z);
        transform.position = driveCollider.transform.position;
    }
    private void FixedUpdate()
    {
        grounded = false;
        RaycastHit hit;

        if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
        {
            grounded = true;
            Quaternion currentTransform = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, currentTransform, snapSpeed);

        }

        emissionRate = 0;

        if (grounded)
        {
            driveCollider.drag = dragOnGround;
            if (Mathf.Abs(speedInput) > 0)
            {
                driveCollider.AddForce(transform.forward * speedInput * boostMultiplier);

                emissionRate = maxEmission;
            }
        }
        else
        {
            driveCollider.drag = 0.1f;
            driveCollider.AddForce(Vector3.up * -gravityForce * 100f);
            emissionRate = 0f;
        }
        foreach (ParticleSystem part in dustTrails)
        {
            var emissionModule = part.emission;
            emissionModule.rateOverTime = emissionRate;
        }
    }
    public void Drift()
    {
        isDrifting = true;
        driftDirection = Input.GetAxis("Horizontal") > 0 ? 1 : 0;
    }
    public IEnumerator Boost()
    {
        Debug.Log("Boost Activated");
        boostMultiplier = 2.5f;
        thrusterFX.SetActive(true);
        AudioManager.audio_instance.PlaySFX(7);
        yield return new WaitForSeconds(boostlength);
        thrusterFX.SetActive(false);
        boostMultiplier = 1;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            other.gameObject.GetComponent<ChargePickup>().Collected();
            //GetComponent<BatteryManager>().currbatteryLife += GetComponent<BatteryManager>().currbatteryLife + 1000f;
        }
        if (other.tag == "ScoreTarget")
        {
            LevelManager.lv_instance.AddScore(boostMultiplier);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - (transform.up * 2));
    }

}

