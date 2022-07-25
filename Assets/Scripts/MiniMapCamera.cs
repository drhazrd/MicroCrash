using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Camera))]
public class MiniMapCamera : MonoBehaviour
{
	Camera minicam;
	CamController mainCam;
	public Transform target;
	Vector3 targetOffset = new Vector3(0,300f,0);
    private void Awake()
    {
		minicam = GetComponent<Camera>();
		mainCam = CamController.cam_instance;
        
    }
	void Start()
	{
		this.transform.parent = null;
		minicam.orthographicSize = 20;
	}

	void Update()
	{
		target = GameManager.gm_instance.targets[0].transform;		
		
		if (Input.GetKeyDown(KeyCode.U) && minicam.orthographicSize >= 20)
		{
			minicam.orthographic = true;
			minicam.orthographicSize += 10;
		}
		if (Input.GetKeyDown(KeyCode.I) && minicam.orthographicSize <= 70)
		{
			minicam.orthographic = true;
			minicam.orthographicSize -= 10;
		}

	}

	void FindTarget()
	{
		if (target)
		{
			return;
		}
		Transform newTarget = CamController.cam_instance.camTarget;
		if (newTarget)
		{
			target = newTarget;
		}
	}
}