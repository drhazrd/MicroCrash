using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    float fovCar = 45f, fovFoot = 15f;
    Camera cam;
    public Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void Update()
    {
        //if (PlayerController.player_instance.onFoot) {
        //    cam.fieldOfView = fovFoot;
        //} else { 
        //    cam.fieldOfView = fovCar;
        //} 
    }
    void LateUpdate()
    {
        transform.position = target.position - cameraOffset;
        transform.rotation = target.rotation;
        transform.LookAt(target);
    }
}
