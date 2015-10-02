using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Damageable
{
	// Update is called once per frame
	public override void UpdateOverride()
	{
		EnemyBehaviour();
	}

	public virtual void EnemyBehaviour()
	{

	}

	public override void OnDeath()
	{
		//if(GameManager.inst.gameDataManager != null)
		GameManager.inst.gameDataManager.allEnemiesKilled.Add(this);

		gameObject.SetActive(false);

	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Player>().OnTakeDamage(GetDamage());
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "WepRight")
		{
			Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();

			OnTakeDamage(wep.GetDamage());
		}

		if (col.gameObject.tag == "Projectile")
		{
			OnTakeDamage(col.gameObject.GetComponent<Projectile>().GetDamage());
			Destroy(col.gameObject);
		}
	}

}
