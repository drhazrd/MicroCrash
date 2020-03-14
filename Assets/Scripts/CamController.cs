using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;
    public Camera mainCamera;
    public Transform camPiviot;
    public Vector3 playerPosition;
    public Vector3 cameraOffset;
    public float viewAngleX, viewAngleY, viewAngleZ;
    // Start is called before the first frame update
    void Start()
    {
        camPiviot.transform.position = target.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //playerPosition = new Vector3 (playerPosition.x + cameraOffset.x, 7f, playerPosition.z + cameraOffset.z);
        transform.LookAt(target);
        transform.position = target.position - cameraOffset;
        mainCamera.transform.rotation = Quaternion.Euler(viewAngleX, viewAngleY, viewAngleZ);
    }
}
