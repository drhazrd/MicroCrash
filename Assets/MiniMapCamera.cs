using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Camera))]
public class MiniMapCamera : MonoBehaviour
{
	Camera minicam;
	Transform target;
	Vector3 targetOffset = new Vector3(0,300f,0);

	void Start()
	{
		minicam = GetComponent<Camera>();
		if (!target)
		{
			target = GameObject.FindWithTag("Player").transform;
		}
	}

	void Update()
	{
		transform.position = target.position + targetOffset;
		if (target)
		{
			FindTarget();
			return;
		}
		if (Input.GetKeyDown(KeyCode.U) && minicam.orthographicSize >= 20)
		{
			minicam.orthographic = true;
			minicam.orthographicSize -= 10;
		}
		if (Input.GetKeyDown(KeyCode.I) && minicam.orthographicSize <= 70)
		{
			minicam.orthographic = true;
			minicam.orthographicSize += 10;
		}

	}

	void FindTarget()
	{
		if (target)
		{
			return;
		}
		Transform newTarget = GameObject.FindWithTag("Player").transform;
		if (newTarget)
		{
			target = newTarget;
		}
	}
}