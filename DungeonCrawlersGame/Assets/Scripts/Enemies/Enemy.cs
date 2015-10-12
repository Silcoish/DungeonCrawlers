using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Damageable
{
	[Header("GLobal Enemy")]
	public RoomObject room;

	public float m_timerPause = 0;
	// Update is called once per frame
	public override void UpdateOverride()
	{
		if (m_timerPause > 0)
		{
			m_timerPause -= Time.deltaTime;
		}
		else
		{
			EnemyBehaviour();
		}
	}

	public virtual void EnemyBehaviour()
	{

	}

	public override void OnDeath()
	{
		//if(GameManager.inst.gameDataManager != null)
		GameManager.inst.gameDataManager.allEnemiesKilled.Add(this);

		if(room != null) room.EnemyDied();
		gameObject.SetActive(false);

	}

	public void PauseEnemy(float seconds)
	{
		m_timerPause = seconds;
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			col.gameObject.GetComponent<Damageable>().OnTakeDamage(GetDamage());
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Weapon")
		{
			OnTakeDamage(GameManager.inst.activeItems.wepSlot1.GetComponent<ItemBase>().GetDamage());
		}

		if (col.gameObject.tag == "Projectile")
		{
			OnTakeDamage(col.gameObject.GetComponent<Damageable>().GetDamage());
			Destroy(col.gameObject);
		}
	}

}
