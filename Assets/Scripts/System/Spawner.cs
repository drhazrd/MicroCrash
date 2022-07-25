using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnCharacter;
    public GameObject spawnObject;
    private Transform spawnPoint;
    public bool onlyObject;

    private void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        Instantiate(spawnCharacter, this.transform.position, this.transform.rotation);
    }
}

