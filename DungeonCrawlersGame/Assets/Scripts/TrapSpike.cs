using UnityEngine;
using System.Collections;

public class TrapSpike : MonoBehaviour {

	[SerializeField] int damage;
	[SerializeField] float cooldown = 0;
	float counter = 0;
	[SerializeField] float knockbackMultiplyer;

	void Update()
	{
		counter += Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (counter > cooldown)
		{
			if (TriggerKnockback(col.gameObject))
			{
				counter = 0;
			}
		}
	}

	bool TriggerKnockback(GameObject go)
	{
		Vector2 kbForce = (go.transform.position - gameObject.transform.position).normalized;
		kbForce.x *= knockbackMultiplyer;
		kbForce.y *= knockbackMultiplyer;
		if (go.tag == "Player")
		{
			print(go.name);
			go.gameObject.GetComponent<Player>().OnTakeDamage(damage, kbForce);
			return true;
		}
		else if (go.tag == "Enemy")
		{
			go.GetComponent<Enemy>().OnTakeDamage(damage, kbForce);
			return true;
		}

		return false;
	}
}
