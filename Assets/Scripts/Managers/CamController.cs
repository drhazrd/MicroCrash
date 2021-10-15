using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public static CamController cam_instance;
    public Transform camTarget;
    public List<CamTarget> targets;
    float fovCar = 65f;
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
        cam.fieldOfView = fovCar;
        FindCameraTargets();
    }
    private void Update()
    {
        UpdateTargets(targets[0].transform);
        /*
        if (camTarget == null)
        {
            Transform player = FindObjectOfType<CamTarget>().transform;
            UpdateTargets(player.transform);
        }
        */
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
