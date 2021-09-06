using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour {
    
	public int startingHealth;
	private int currentHealth;

	public float flashLength;
	private float flashCounter;

	private Renderer rend;
	private Color storedColor;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		rend = GetComponentInChildren<Renderer>();
		storedColor = rend.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		if(currentHealth <=0)
		{
			LevelManager.lv_instance.MovementFreeze();
			gameObject.SetActive(false);
		}

		/*if(flashCounter > 0)
		{
			flashCounter -= Time.deltaTime;
			if(flashCounter <= 0)
			{
				rend.material.SetColor("_Color", storedColor);
			}
		}*/
	}

	public void HurtPlayer(int damageAmount)
	{
		currentHealth -= damageAmount;
		//flashCounter = flashLength;
		//rend.material.SetColor("_Color", Color.white);
	}
}