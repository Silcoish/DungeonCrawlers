using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour 
{
	public string name;
	public int hitPoints = 10;
	public DamageType effectType = DamageType.NONE;
	public float effectDuration = 1;
	public float effectStrength = 1;
	public int collisionDamage = 1;
	public float knockbackForce = 1;

	float timerPoison = 0;
	float timerFire = 0;
	float timerIce = 0;
	float timerMud = 0;
	float damageTimer = 0;

	public float effectFlashRate = 0.2f;

	SpriteRenderer sp;

	private Rigidbody2D rb;

	void Awake () 
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		sp = gameObject.GetComponent<SpriteRenderer>();
	}
	
	void Update () 
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


		UpdateOverride();
	}

	public virtual void UpdateOverride()
	{

	}


	public Damage GetDamage()
	{
		Damage temp;

		temp.type = effectType;
		temp.amount = collisionDamage;
		temp.knockback = knockbackForce;
		temp.fromGO = gameObject.transform;
		temp.effectTime = effectDuration;
		temp.effectStrength = effectStrength;

		return temp;
	}

	float SinLerp(float tmr)
	{
		return (1 + Mathf.Sin(tmr / effectFlashRate)) / 2;
	}

	/// <summary>
	/// Called When the Enemy Takes any damage/hit
	/// Damge could be = to 0
	/// </summary>
	/// <param name="dam"></param>
	public void OnTakeDamage(Damage dam)
	{
		hitPoints -= dam.amount;

		damageTimer = 1;

		if (hitPoints <= 0)
		{
			OnDeath();
		}
	}

	public virtual void OnDeath()
	{
		Destroy(gameObject);
	}

	public string GetName()
	{
		return name;
	}


}
