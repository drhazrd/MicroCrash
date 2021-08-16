﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
	public float moveSpeed, sprintSpeed;
	private Rigidbody myRigidbody;

	private Vector3 moveInput;
	private Vector3 moveVelocity;

	private Camera mainCamera;

	GunController theGun;

	public bool useController;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody>();
		mainCamera = FindObjectOfType<Camera>();
		theGun = GetComponentInChildren<GunController>();
	}
	
	// Update is called once per frame
	void Update () {
		moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
		moveVelocity = moveInput * moveSpeed;

		//Rotate with Mouse
		if(!useController)
		{
			Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
			Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
			float rayLength;

			if(groundPlane.Raycast(cameraRay, out rayLength))
			{
				Vector3 pointToLook = cameraRay.GetPoint(rayLength);
				Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

				transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
			}

			if(Input.GetMouseButtonDown(0))
				theGun.isFiring = true;

			if(Input.GetMouseButtonUp(0))
				theGun.isFiring = false;

		}

		//Rotate with Controller
		if(useController)
		{
			Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("JoyRightHoz") + Vector3.forward * -Input.GetAxisRaw("JoyRightVert");
			if(playerDirection.sqrMagnitude > 0.0f)
			{
				transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
			}

			if(Input.GetKeyDown(KeyCode.Joystick1Button5))
				theGun.isFiring = true;

			if(Input.GetKeyUp(KeyCode.Joystick1Button5))
				theGun.isFiring = false;
		}
	}

	void FixedUpdate () {
		myRigidbody.velocity = moveVelocity;
	}
}