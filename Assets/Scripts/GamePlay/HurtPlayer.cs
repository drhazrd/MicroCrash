﻿using UnityEngine;
using System.Collections;

public class HurtPlayer : MonoBehaviour {
    
	public int damageToGive;

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerHealthManager>().HurtPlayer(damageToGive);
		}
	}
}