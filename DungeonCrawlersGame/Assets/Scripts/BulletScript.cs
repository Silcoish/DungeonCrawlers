using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{
	public int damage = 0;
	public float knockbackForce = 1;



	void OnCollisionEnter2D(Collision2D col)
	{
		print("Colision");
		if (col.collider.tag == "Wall")
		{
			Destroy(gameObject);
		}
		else if (col.collider.tag == "Player")
		{
			//TODO Link up damage from prefab input
			Vector2 kbForce = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
			col.gameObject.GetComponent<Player>().OnTakeDamage(damage, kbForce * knockbackForce);
			Destroy(gameObject);
		}
		else if (col.collider.tag == "Player" || col.collider.tag == "Enemy")
		{
			Vector2 kbForce = gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
			col.gameObject.GetComponent<Enemy>().OnTakeDamage(damage, kbForce * knockbackForce);
			Destroy(gameObject);
		}

	}
}
