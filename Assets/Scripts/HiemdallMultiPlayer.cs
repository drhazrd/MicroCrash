using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof (Camera))]
public class HiemdallMultiPlayer : MonoBehaviour
{
    private List<TestCarScript> targets;
    private TestCarScript target;
    private Vector3 camOffset = new Vector3(0,25f,-25f);
   [SerializeField]
    private Vector3 velocity;
    private float smoothTime = .5f;
    public float minZoom = 40;
    public float maxZoom = 10;
    public float zoomLimiter = 50f;
    private Camera _gameCam;
    [SerializeField]
    public static HiemdallMultiPlayer cam_Instance { get; private set; }

    private void Awake()
    {
        if (cam_Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            cam_Instance = this;
            DontDestroyOnLoad(cam_Instance);
        }

    }
    void Start()
    {
        _gameCam = GetComponent<Camera>();
        targets = new List<TestCarScript>();
    }
    void LateUpdate()
    {
        if (targets.Count == 0)
        {
            UpdateTargets();
        }
        Zoom();
        Move();
    }
    public void UpdateTargets()
    {
        targets.AddRange(FindObjectsOfType<TestCarScript>());
    }
    public void UpdateTargets(TestCarScript singlePlayer)
    {
        target = singlePlayer;
    }
    public void CameraShake(float shakeDur,float shakeAmt)
    {
        StartCoroutine(Shake(shakeDur,shakeAmt));
    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        _gameCam.fieldOfView = Mathf.Lerp(_gameCam.fieldOfView, newZoom, Time.deltaTime);
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 camPosition = centerPoint + camOffset;
        transform.position = Vector3.SmoothDamp(transform.position, camPosition, ref velocity, smoothTime);
        transform.LookAt(centerPoint);
    }
    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].gameObject.transform.position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].gameObject.transform.position);
        }
        return bounds.size.z;
    }
    private Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].gameObject.transform.position;
        }

        var bounds = new Bounds(targets[0].gameObject.transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].gameObject.transform.position);
        }
        return bounds.center;
    }
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 orgPos = transform.localPosition + camOffset;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, orgPos.y, orgPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = orgPos;
        Debug.Log("Shake");
    }
}

public class cameraShake : MonoBehaviour
{
}
