using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	string name;

	public int health;

	public int collisionDamage = 1;
	public float knockbackForce = 0;

	SpriteRenderer sp;


	private Rigidbody2D rb;


	float timerPoison = 0;
	float timerFire = 0;
	float timerIce = 0;
	float timerMud = 0;
	float damageTimer = 0;

	public float effectFlashRate = 0.2f;


	// Use this for initialization
	void Awake()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		sp = gameObject.GetComponent<SpriteRenderer>();

	}

	public string GetName()
	{
		return name;
	}

	float SinLerp(float tmr)
	{
		return (1 + Mathf.Sin(tmr / effectFlashRate)) / 2;
	}


	// Update is called once per frame
	void FixedUpdate()
	{
		//Colour Change for taking damage.
		if (damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
			Color spColor = Color.Lerp(Color.white, Color.red, damageTimer);
			sp.color = spColor;
		}

		if (timerPoison > 0)
		{
			timerPoison -= Time.deltaTime;
			Color spColor = Color.Lerp(Color.white, Color.green, SinLerp(timerPoison));
			sp.color = spColor;
		}

		if (timerFire > 0)
		{


		}

		if (timerIce > 0)
		{


		}

		if (timerMud > 0)
		{


		}
	}

	/// <summary>
	/// Called When the Enemy Takes any damage/hit
	/// Damge could be = to 0
	/// </summary>
	/// <param name="dam"></param>
	public void OnTakeDamage(Damage dam)
	{
		health -= dam.amount;

		damageTimer = 1;

		if (health <= 0)
		{
			OnDeath();
		}
	}

	public void OnDeath()
	{
		//if(GameManager.inst.gameDataManager != null)
		GameManager.inst.gameDataManager.allEnemiesKilled.Add(this);

		if (GameManager.inst.questManager.currentQuest != null)
			DisplayQuestText();

		gameObject.SetActive(false);

	}

	void DisplayQuestText()
	{
		GameObject g = new GameObject("Quest Text");
		g.transform.position = GameManager.inst.player.transform.position;
		g.AddComponent<QuestText>();
		TextMesh tm = g.AddComponent<TextMesh>();
		tm.characterSize = 0.2f;
		tm.fontSize = 25;
		tm.color = Color.red;
		tm.text = GameManager.inst.questManager.currentQuest.GetQuestCounterText();
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Vector2 kbForce = (col.gameObject.transform.position - gameObject.transform.position).normalized;
			col.gameObject.GetComponent<Player>().OnTakeDamage(collisionDamage, kbForce * knockbackForce);
		}
		if (col.gameObject.tag == "Enemy")
		{
			//Enmemies do not take collision damage from other enemies.
		}

		// If we standardize the damage call
		//col.gameObject.GetComponent<Damageable>().TakeDamage(damage, transform.position, knockback);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		//if (col.gameObject.tag == "WepLeft")
		//{
		//    Weapon wep = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>();
		//    Vector2 kbForce = -(col.gameObject.transform.position - gameObject.transform.position).normalized;
		//    OnTakeDamage(wep.dmg, kbForce);// * wep.kb);
		//}
		//if (col.gameObject.tag == "WepRight")
		//{
		//	Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();
		//	Vector2 kbForce = -(col.gameObject.transform.position - gameObject.transform.position).normalized;
		//	OnTakeDamage(wep.dmg, kbForce);// * wep.kb);
		//}

		//if(col.gameObject.tag == "Projectile")
		//{
		//	Vector2 kbForce = -(col.gameObject.transform.position - gameObject.transform.position).normalized;
		//	OnTakeDamage(col.gameObject.GetComponent<Projectile>().dmg, kbForce);
		//	Destroy(col.gameObject);
		//}
	}

	//void OnTriggerEnter2D(Collider2D col)
	//{
	//	if (col.gameObject.tag == "WepLeft")
	//	{
	//		Weapon wep = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>();
	//		//TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
	//		OnTakeDamage(col.transform.position, wep.dmg);
	//	}
	//	if (col.gameObject.tag == "WepRight")
	//	{
	//		Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();
	//		//TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
	//		OnTakeDamage(col.transform.position, wep.dmg);
	//	}
	//}
}
