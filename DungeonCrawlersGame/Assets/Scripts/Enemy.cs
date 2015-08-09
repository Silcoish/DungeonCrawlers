﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour 
{
	[SerializeField]
	public List<Behaviour> enemyBehaviours = new List<Behaviour>();

	public int health;


	private Rigidbody2D rb;

	public float moveSpeed;
	public Vector2 lookDir;
	public Vector2 moveDir;


	// Use this for initialization
	void Awake () 
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		moveSpeed = 0;
		lookDir = Vector2.up;
		moveDir = Vector2.up;
	}

	void Start()
	{
		for (int i = 0; i < enemyBehaviours.Count; i++)
		{
			enemyBehaviours[i].BehaviourStart();
		}

	}
	
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		//Reset all variables
		ResetSpeed();
		//ResetLookDirection();
		//ResetMoveDirection();
		
		//Run all Behaviours
		for (int i = 0; i < enemyBehaviours.Count; i++)
		{
			enemyBehaviours[i].BehaviourUpdate(this);
		}

		//Set enemies Movement.
		gameObject.transform.rotation = Quaternion.FromToRotation(Vector2.up, lookDir);
		rb.velocity = moveDir.normalized * moveSpeed;
	}


	/// <summary>
	/// All Getters
	/// </summary>
	/// <returns></returns>
	public float GetSpeed()
	{
		return moveSpeed;
	}

	public Vector2 GetLookDirection()
	{
		return lookDir;
	}

	public Vector2 GetMoveDirection()
	{
		return moveDir;
	}
	public float GetRbVelocity()
	{
		return rb.velocity.magnitude;
	}



	/// <summary>
	/// All Setters
	/// </summary>
	/// <param name="sp"></param>
	public void SetSpeed(float sp)
	{
		if (sp > moveSpeed)
		{
			moveSpeed = sp;
		}
	}

	public void SetLookDirection(float angle)
	{
		lookDir = GLobalFunctions.DegToVector(angle);
	}

	public void RotateLookDirection(float angle)
	{
		GLobalFunctions.RotateVector(ref lookDir, angle);
	}

	public void SetMoveDirection(float angle)
	{
		moveDir = GLobalFunctions.DegToVector(angle).normalized;
	}

	public void SetMoveDirection(Vector2 m_dir)
	{
		moveDir = m_dir.normalized;
	}

	public void RotateMoveDirection(float angle)
	{
		GLobalFunctions.RotateVector(ref moveDir, angle);
	}


	/// <summary>
	/// Resets the speed to the speed at the start of the update.
	/// </summary>
	public void ResetSpeed()
	{
		moveSpeed = rb.velocity.magnitude;
	}
	public void ResetLookDirection()
	{
		lookDir = transform.rotation * Vector2.up;
	}
	public void ResetMoveDirection()
	{
		moveDir = rb.velocity.normalized;
	}


	/// <summary>
	/// Called When the Enemy Takes any damage/hit
	/// Damge could be = to 0
	/// </summary>
	/// <param name="dam"></param>
	public void OnTakeDamage(Vector2 origin, int dam)
	{
		health -= dam;

		//Loop through Behaviours and call on take damage
		for (int i = 0; i < enemyBehaviours.Count; i++)
		{
			enemyBehaviours[i].OnTakeDamage(origin, dam);
		}


		if (health <= 0)
		{
			OnDeath();
		}
	}

	public void OnDeath()
	{
		//Loop through Behaviours and call on death
		for (int i = 0; i < enemyBehaviours.Count; i++)
		{
			enemyBehaviours[i].OnDeath();
		}

		Destroy(gameObject);

	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().TakeDamage(0, transform.position, 5);
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().OnTakeDamage(transform.position, 0);
        }

        // If we standardize the damage call
        //col.gameObject.GetComponent<Damageable>().TakeDamage(damage, transform.position, knockback);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "WepLeft")
        {
            Weapon wep = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>();
            //TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
            OnTakeDamage(col.transform.position, wep.dmg);
        }
        if (col.gameObject.tag == "WepRight")
        {
            Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();
            //TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
            OnTakeDamage(col.transform.position, wep.dmg);
        }
    }
}
