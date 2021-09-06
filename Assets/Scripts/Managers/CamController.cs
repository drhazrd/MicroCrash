using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController cam_instance;
    public Transform camTarget;
    public List<CamTarget> targets;
    float fovCar = 45f, fovFoot = 15f;
    Camera cam;
    bool playerFound;
    Vector3 cameraOffset = new Vector3(0, -10, 10);
    private void Awake()
    {
        cam_instance = this;
    }
    void Start()
    {
        cam = GetComponent<Camera>();
        FindCameraTargets();
    }
    private void Update()
    {
        playerFound = camTarget != null;
        if (playerFound)
        {
            if (camTarget.gameObject.tag == "KartBody")
            {
                cam.fieldOfView = fovCar;
                Debug.Log("Car Camera");
            }
            else
            if (camTarget.gameObject.tag == "Player")
            {
                cam.fieldOfView = fovFoot;
                Debug.Log("Foot Camera");
            }
        }
        if (camTarget == null)
        {
            Transform player = FindObjectOfType<PlayerController>().transform;
            UpdateTargets(player.transform);
        }
    }
    void LateUpdate()
    {
        transform.position = camTarget.position - cameraOffset;
        transform.rotation = camTarget.rotation;
        transform.LookAt(camTarget);
    }
    void FindCameraTargets()
    {
        targets.AddRange(FindObjectsOfType<CamTarget>());
    }
    public void UpdateTargets(Transform newTarget)
    {
        camTarget = newTarget;
         
    }
}
