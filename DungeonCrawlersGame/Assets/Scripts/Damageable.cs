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
	public float globalMoveSpeed = 1;
	public float globalBlindSpeed = 1;

	float poisonTime = 1f;//Time inbetween poison hits.
	float timerPoisonHits = 0;

	public float timerPoison = 0;
	public float timerBurn = 0;
	public float timerFreeze = 0;
	public float timerMud = 0;
	public float timerBleed = 0;
	public float timerBlind = 0;
	public float damageTimer = 0;


	public float strengthPoison = 0;
	public float strengthBurn = 0;
	public float strengthBleed = 1;//damage multiplyer
	public float strengthMud = 1;
	public float strengthFreeze = 1;

	float leftoverBurnDamage = 0;


	public float effectFlashRate = 0.2f;

	protected SpriteRenderer sp;

	protected Rigidbody2D rb;

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
			sp.color = Color.Lerp(Color.white, Color.red, damageTimer);
		}

		//POISON
		if (timerPoison > 0)
		{
			timerPoison -= Time.deltaTime;
			sp.color = Color.Lerp(Color.white, Color.green, SinLerp(timerPoison));

			timerPoisonHits += Time.deltaTime;

			if (timerPoisonHits > poisonTime)
			{
				DamagePoison();
				timerPoisonHits = 0;
			}
		}

		//BURN
		if (timerBurn > 0)
		{
			timerBurn -= Time.deltaTime;
			sp.color = Color.Lerp(Color.white, Color.red, SinLerp(timerPoison));

			DamageBurn();

		}

		//FREEZE
		if (timerFreeze > 0)
		{
			timerFreeze -= Time.deltaTime;
			sp.color = Color.Lerp(Color.white, Color.blue, SinLerp(timerPoison));

			globalMoveSpeed = 0;

			if (timerFreeze <= 0)
			{
				globalMoveSpeed = 1;
			}
		}

		//MUD
		if (timerMud > 0)
		{
			globalMoveSpeed = 0;

			if (timerFreeze <= 0)
			{
				globalMoveSpeed = 1;
			}
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

        Vector2 kbForce = (transform.position - dam.fromGO.position).normalized;
        rb.AddForce(kbForce, ForceMode2D.Impulse);

		switch (dam.type)
		{
			case DamageType.NONE:
				break;
			case DamageType.POISON:
				timerPoison = dam.effectTime;
				strengthPoison = dam.effectStrength;
				break;
			case DamageType.BURN:
				timerBurn = dam.effectTime;
				strengthBurn = dam.effectStrength;
				break;
			case DamageType.FREEZE:
				timerFreeze = dam.effectTime;
				strengthFreeze = dam.effectStrength;
				break;
			case DamageType.BLEED:
				timerBleed = dam.effectTime;
				strengthBleed = dam.effectStrength;
				break;
		}

		if (timerBleed > 0)
		{
			DamageBleed(dam.amount);
		}

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

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// ######## ##       ######## ##     ## ######## ##    ## ########    ###    ##          ########     ###    ##     ##    ###     ######   ########	//
	// ##       ##       ##       ###   ### ##       ###   ##    ##      ## ##   ##          ##     ##   ## ##   ###   ###   ## ##   ##    ##  ##		//
	// ##       ##       ##       #### #### ##       ####  ##    ##     ##   ##  ##          ##     ##  ##   ##  #### ####  ##   ##  ##        ##		//
	// ######   ##       ######   ## ### ## ######   ## ## ##    ##    ##     ## ##          ##     ## ##     ## ## ### ## ##     ## ##   #### ######	//
	// ##       ##       ##       ##     ## ##       ##  ####    ##    ######### ##          ##     ## ######### ##     ## ######### ##    ##  ##		//
	// ##       ##       ##       ##     ## ##       ##   ###    ##    ##     ## ##          ##     ## ##     ## ##     ## ##     ## ##    ##  ##		//
	// ######## ######## ######## ##     ## ######## ##    ##    ##    ##     ## ########    ########  ##     ## ##     ## ##     ##  ######   ########	//
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void DamagePoison()
	{
		hitPoints -= (int)(strengthPoison * poisonTime);
	}

	void DamageBurn()
	{
		leftoverBurnDamage += strengthBurn * Time.deltaTime;
		hitPoints -= (int)leftoverBurnDamage;
		leftoverBurnDamage -= (int)leftoverBurnDamage;
	}

	void DamageBleed(int damIn)
	{
		hitPoints -= (int)(damIn * strengthBleed);
	}

}
