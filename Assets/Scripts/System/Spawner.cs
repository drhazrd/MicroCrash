using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject character;
    private Transform spawnPoint;
    void Start()
    {
        Instantiate(character, this.transform.position, this.transform.rotation);
    }
}

