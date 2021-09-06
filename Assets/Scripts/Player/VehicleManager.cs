using UnityEngine;
using System.Collections;

public class VehicleManager : MonoBehaviour
{

	private bool inVehicle = false;
	RCController vehicleScript;
	CamTarget camTarget;
	GameObject player;
	public GameObject carObj;
	public GameObject carCollider;


	void Start()
	{
		vehicleScript = GetComponentInParent<RCController>();
		camTarget = vehicleScript.gameObject.GetComponent<CamTarget>();
		camTarget.enabled = inVehicle;
		//if()player = GameManager.gm_instance.thePlayer.gameObject;
	}

	// Update is called once per frame
	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && inVehicle == false)
		{
			if (player == null)
			{
				player = other.gameObject;
			}
			else
			if (Input.GetKeyDown(KeyCode.E) && player != null)
			{
				EnterVehicle();
				return;
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
				player = null;
		}
	}
	void Update()
	{
		camTarget.enabled = inVehicle;
		if (inVehicle == true && Input.GetKeyDown(KeyCode.F))
		{
			ExitVehicle();
		}
	}
	void EnterVehicle()
	{
		vehicleScript.enabled = true;
		inVehicle = true;
		Debug.Log("Got In the Car");
		player.transform.parent = carObj.transform;
		player.transform.position = carObj.transform.position;
		player.SetActive(false);
		player.GetComponentInChildren<CamTarget>().enabled = false;
		CamController.cam_instance.UpdateTargets(carCollider.transform);
		carCollider.SetActive(true);
		//carObj.SetActive(true);
	}
	void ExitVehicle()
	{
		vehicleScript.enabled = false;
		inVehicle = false;
		Debug.Log("Got Out the Car");
		player.transform.parent = null;
		player.SetActive(true);
		player.GetComponentInChildren<CamTarget>().enabled = true;
		CamController.cam_instance.UpdateTargets(player.transform);
		carCollider.SetActive(false);
		//carObj.SetActive(false);
	}
}

