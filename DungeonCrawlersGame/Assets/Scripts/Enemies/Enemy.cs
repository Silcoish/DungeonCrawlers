using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : Damageable
{
	[Header("GLobal Enemy")]
	public RoomObject room;

	public float m_timerPause = 0;

    private Damage lastDamage; // Get the damage data when hit, but call OnTakeDamage after delay
    private bool takeDamage;
    private float pauseDelay = 0.25F;

	// Update is called once per frame
	public override void UpdateOverride()
	{
        // Check if behaviour pause has ended
        if (m_timerPause < 0)
        {
            // Do we need to take damage?
            if (takeDamage)
            {
                // Take damage, be knocked away, continue to delay behaviour
                OnTakeDamage(lastDamage);
                takeDamage = false;
                PauseEnemy(pauseDelay);
            }
            else
            {
                // We've taken damage, been knocked back, and can now continue behaviour
                EnemyBehaviour();
            }
        }
        m_timerPause -= Time.deltaTime;
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
			//OnTakeDamage(GameManager.inst.activeItems.wepSlot1.GetComponent<ItemBase>().GetDamage());

            lastDamage = GameManager.inst.activeItems.wepSlot1.GetComponent<ItemBase>().GetDamage();
            takeDamage = true;
            PauseEnemy(pauseDelay);
		}

		if (col.gameObject.tag == "Projectile")
		{
			//OnTakeDamage(col.gameObject.GetComponent<Damageable>().GetDamage());

            lastDamage = GameManager.inst.activeItems.wepSlot1.GetComponent<ItemBase>().GetDamage();
            takeDamage = true;
            PauseEnemy(pauseDelay);

			Destroy(col.gameObject);
		}
	}

}
