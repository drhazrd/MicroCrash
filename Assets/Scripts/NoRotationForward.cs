using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRotationForward : MonoBehaviour
{
    public Transform m_test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 noRotationForward = Vector3.Normalize(new Vector3(transform.forward.x, 0, transform.forward.z));
        m_test.position = noRotationForward + transform.position;
    }
}
