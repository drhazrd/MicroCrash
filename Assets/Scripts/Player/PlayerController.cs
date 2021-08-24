using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController player_instance;
    float moveSpeed;
    float walkSpeed = 1.2f;
    float runSpeed = 4f;
    float distFromGround;
    [SerializeField]
    float jumpForce = 10f;
    CharacterController controller;
    private Vector3 moveDirection, aimDirection;
    public float gravityScale;
    Animator anim;
    Transform playerPivotPoint;
    public float rotateSpeed;
    GameObject characterGraphics;
    bool player1, player2, player3, player4;
    public float knockBackForce;
    public float knockBackTime;
    private float knockBackCounter;
    bool canMove;
    private bool isMoving = false;
    private bool isRunning = false;
    private bool canInteract = true;
    public bool onFoot = true;
    private float aimVertical, aimHorizontal;
    private bool isAiming;

    private void Awake()
    {
        player_instance = this;
    }
    void Start()
    {
        controller = GetComponent<CharacterController>();
        moveSpeed = walkSpeed;
        characterGraphics = this.transform.GetChild(0).gameObject;
        playerPivotPoint = this.transform.GetChild(1);
        anim = characterGraphics.GetComponent<Animator>();
    }
    void Update()
    {
        anim.SetBool("canMove", canMove);
        canMove = LevelManager.lv_instance.movementFreeze;

        if (knockBackCounter <= 0)
        {
            float yStore = moveDirection.y;
            if (canMove == true)
            {
                moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
                moveDirection = moveDirection.normalized * moveSpeed;
                aimDirection = new Vector3(aimVertical, 0, aimHorizontal);
            }
            else
            {
                return;
            }
            moveDirection.y = yStore;
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                //Improved jump script.
                if (Input.GetButtonDown("Jump"))
                {
                    Jump();
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
        }

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
        distFromGround = moveDirection.y;
        UpdateAnimation();
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            MoveInput();
        }
        else if (Input.GetAxis("Horizontal") == 0f || Input.GetAxis("Vertical") == 0f)
        {
            isMoving = false;
            isRunning = false;
            moveSpeed = walkSpeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.Joystick1Button8))
        {
            if (isMoving)
            {
                isRunning = true;
                moveSpeed = runSpeed;
            }
            else if (!isMoving)
            {
                moveSpeed = walkSpeed;
                isRunning = false;
            }
        }

        canInteract = false;

        if (Input.GetAxis("JoyRightHoz") != 0 || Input.GetAxis("JoyRightVert") != 0)
        {
            isAiming = true;
        }
        else if (Input.GetAxis("JoyRightHoz") == 0 || Input.GetAxis("JoyRightVert") == 0)
        {
            isAiming = false;
        }
    }
    void MoveInput()
    {
        transform.rotation = Quaternion.Euler(0f, playerPivotPoint.rotation.eulerAngles.y, 0);
        Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
        if (isAiming)
        {
            characterGraphics.transform.rotation = Quaternion.Slerp(characterGraphics.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }
        else if (!isAiming)
        {
            characterGraphics.transform.rotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("JoyRightHoz"), 0f, Input.GetAxis("JoyRightVert")));
        }
        isMoving = true;

    }

    public void Jump()
    {
        moveDirection.y = jumpForce;
        anim.SetTrigger("Jump");
    }
    public void Knockback(Vector3 direction)
    {
        knockBackCounter = knockBackTime;
        moveDirection = direction * knockBackForce;
        moveDirection.y = knockBackForce;
    }
    public void Warp(Vector3 position, Vector3 rotation)
    {
        controller.enabled = false;
        transform.position = position;
        transform.eulerAngles = rotation;
        controller.enabled = true;
    }
    private void UpdateAnimation()
    {
        anim.SetBool("isAiming", isAiming);
        anim.SetFloat("distanceFromGround", distFromGround);
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", controller.isGrounded);
        anim.SetFloat("runSpeed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

}
