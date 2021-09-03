using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    
	public float speed;

	public float lifeTime;

	public int damageToGive;
	
	Light light;

	MeshRenderer bulletRender;

	public Material bulletColor;
	// Use this for initialization
	void Start () {
		light = GetComponentInChildren<Light>();
		bulletRender = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		light.color = bulletColor.color;

		bulletRender.material = bulletColor;

		transform.Translate(Vector3.forward * speed * Time.deltaTime);

		lifeTime -= Time.deltaTime;
		if(lifeTime <= 0)
		{
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(UnityEngine.Collision other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			other.gameObject.GetComponent<EnemyHealthManager>().HurtEnemy(damageToGive);
			Destroy(gameObject);
		}
	}
}