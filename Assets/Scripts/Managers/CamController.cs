using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Transform target;

    public Vector3 cameraOffset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - cameraOffset;
        transform.rotation = target.rotation;
        transform.LookAt(target);
    }
}
