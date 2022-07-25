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
    GameManager gameManager;
    bool playerFound;
    Vector3 cameraOffset = new Vector3(0, -10, 10);
    private void Awake()
    {
        if (cam_instance == null)
        {
            cam_instance = this;
        }
        else
        {
            Destroy(this);
        }
        gameManager = GameManager.gm_instance;
    }
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = fovCar;
    }
    private void Update()
    {
        if (targets.Count < 1)
        {
            camTarget = gameManager.targets[0].transform;
        }
    }
    void LateUpdate()
    {
        if (camTarget != null)
        {
            UpdateTargetPOS();
        }
    }
    public void UpdateTargetPOS()
    {
        transform.position = camTarget.position - cameraOffset;
        transform.rotation = camTarget.rotation;
        transform.LookAt(camTarget);
    }
    public void UpdateTargets(Transform newTarget)
    {
        camTarget = newTarget;
        
    }
}
