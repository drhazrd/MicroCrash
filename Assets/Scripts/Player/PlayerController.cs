using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player_instance;
    float moveSpeed= 2f;
    float walkSpeed = 1.2f;
    float runSpeed = 4f;
    private Rigidbody myRigidbody;

    private Vector3 moveInput, moveVelocity, aimPos;
    Transform cam;
    Vector3 camForward;
    Vector3 move;
    float forwardAmount;
    float turnAmount;
    GunController theGun;

    private Camera mainCamera;
    CamTarget camTarget;
    Animator anim;
    public float rotateSpeed;
    GameObject characterGraphics;
    bool canMove;
    bool useController;
    private bool isMoving = false;
    private bool isRunning = false;
    private bool canInteract = true;
    public bool onFoot = true;
    private bool isAiming;

    private void Awake()
    {
        player_instance = this;
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = CamController.cam_instance.GetComponent<Camera>();
        characterGraphics = this.transform.GetChild(0).gameObject;
        anim = characterGraphics.GetComponent<Animator>();
        camTarget = GetComponentInChildren<CamTarget>();
        cam = mainCamera.transform;
        theGun = GetComponentInChildren<GunController>();

    }
    private void Update()
    {
        onFoot = camTarget.enabled;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            aimPos = hit.point;
        }
        Vector3 aimDir = aimPos - transform.position;
        aimDir.y = 0;

        transform.LookAt(transform.position + aimDir, Vector3.up);
        if (Input.GetMouseButtonDown(0))
            theGun.isFiring = true;

        if (Input.GetMouseButtonUp(0))
            theGun.isFiring = false;
    }
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (cam != null)
        {
            camForward = Vector3.Scale(cam.up, new Vector3(1, 0, 1)).normalized;
            move = vertical * camForward + horizontal * cam.right;
        }
        else
        {
            move = vertical * Vector3.forward + horizontal * Vector3.right;

        }
        if (move.magnitude > 1)
        {
            move.Normalize();
        }
        Move(move);
        Vector3 movement = new Vector3(horizontal, 0, vertical);

        myRigidbody.velocity = movement * moveSpeed;
    }
    void Move(Vector3 move)
    {
        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        this.moveInput = move;

        ConvertMoveInput();
        UpdateAnimation();
    }

    void ConvertMoveInput()
    {
        Vector3 localmove = transform.InverseTransformDirection(moveInput);
        turnAmount = localmove.x;
        forwardAmount = localmove.z;
    }
    void UpdateAnimation()
    {
        anim.SetFloat("Forward", forwardAmount, 1.0f, Time.deltaTime);
        anim.SetFloat("Turn", turnAmount, 1.0f, Time.deltaTime);
    }

}
