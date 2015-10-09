using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour 
{
	[Header("Damageable")]
	public string unitName;
	public int hitPoints = 10;
	public DamageType effectType = DamageType.NONE;
	public float effectDuration = 1;
	public float effectStrength = 1;
	public int collisionDamage = 1;
	public float knockbackForce = 5;
	public float globalMoveSpeed = 1;
	public float globalBlindSpeed = 1;

	private Color startColor;

	float poisonTime = 1f;//Time inbetween poison hits.
	float timerPoisonHits = 0;

	protected float timerPoison = 0;
	protected float timerBurn = 0;
	protected float timerFreeze = 0;
	protected float timerMud = 0;
	protected float timerBleed = 0;
	protected float timerBlind = 0;
	protected float damageTimer = 0;


	protected float strengthPoison = 0;
	protected float strengthBurn = 0;
	protected float strengthBleed = 1;//damage multiplyer
	protected float strengthMud = 1;
	protected float strengthFreeze = 1;

	float leftoverBurnDamage = 0;
	float leftoverPoisonDamage = 0;


	public float effectFlashRate = 0.2f;

	protected SpriteRenderer sp;

	protected Rigidbody2D rb;
	
	

	void Awake () 
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
		sp = gameObject.GetComponent<SpriteRenderer>();
		startColor = sp.color;
	}
	
	public virtual void Update () 
	{
		sp.color = Color.white;

		//Colour Change for taking damage.
		if (damageTimer > 0)
		{
			damageTimer -= Time.deltaTime;
			sp.color = Color.Lerp(startColor, Color.red, damageTimer);
		}

		//POISON
		if (timerPoison > 0)
		{
			timerPoison -= Time.deltaTime;
			sp.color = Color.Lerp(sp.color, Color.green, SinLerp(timerPoison));

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
			sp.color = Color.Lerp(sp.color, Color.red, SinLerp(timerPoison));

			DamageBurn();

		}

		//FREEZE
		if (timerFreeze > 0)
		{
			timerFreeze -= Time.deltaTime;
			sp.color = Color.Lerp(sp.color, Color.blue, SinLerp(timerPoison));

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

        if (hitPoints <= 0)
        {
            OnDeath();
        }

		UpdateOverride();
	}

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    print("Trig");
    //    if (col.gameObject.tag == "WepRight")
    //    {
    //        print("WepRight");
    //        OnTakeDamage(GameManager.inst.activeItems.wepSlot1.GetComponent<Weapon>().GetDamage());
    //    }

    //}

    //void OnCollisionEnter2D(Collision2D col)
    //{
    //    print("Col");

    //}



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

		//AudioManager.Inst.PlaySFX(AudioManager.Inst.a_takeDamage);

        Vector2 kbForce = (transform.position - dam.fromGO.position).normalized * dam.knockback;
        rb.AddForce(kbForce, ForceMode2D.Impulse);

		switch (dam.type)
		{
			case DamageType.NONE:
				break;
			case DamageType.POISON:
				AudioManager.Inst.PlaySFX(AudioManager.Inst.a_poison);
				timerPoison = dam.effectTime;
				strengthPoison = dam.effectStrength;
				break;
			case DamageType.BURN:
				AudioManager.Inst.PlaySFX(AudioManager.Inst.a_burnt);
				timerBurn = dam.effectTime;
				strengthBurn = dam.effectStrength;
				break;
			case DamageType.FREEZE:
				AudioManager.Inst.PlaySFX(AudioManager.Inst.a_frozen);
				timerFreeze = dam.effectTime;
				strengthFreeze = dam.effectStrength;
				break;
			case DamageType.BLEED:
				AudioManager.Inst.PlaySFX(AudioManager.Inst.a_bleed);
				timerBleed = dam.effectTime;
				strengthBleed = dam.effectStrength;
				break;
			default:
				AudioManager.Inst.PlaySFX(AudioManager.Inst.a_stab);
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
		return unitName;
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
		leftoverPoisonDamage += strengthPoison * poisonTime;
		hitPoints -= (int)leftoverPoisonDamage;
		if ((int)leftoverPoisonDamage > 0)
			SpawnText(Color.green, ((int)leftoverPoisonDamage).ToString());
		leftoverPoisonDamage -= (int)leftoverPoisonDamage;

		AudioManager.Inst.PlaySFX(AudioManager.Inst.a_poison);
	}

	void DamageBurn()
	{
		leftoverBurnDamage += strengthBurn * Time.deltaTime;
		hitPoints -= (int)leftoverBurnDamage;
		if ((int)leftoverBurnDamage > 0)
			SpawnText(Color.red, ((int)leftoverBurnDamage).ToString());
		leftoverBurnDamage -= (int)leftoverBurnDamage;

		AudioManager.Inst.PlaySFX(AudioManager.Inst.a_burnt);
	}

	void DamageBleed(int damIn)
	{
		hitPoints -= (int)(damIn * strengthBleed);

		AudioManager.Inst.PlaySFX(AudioManager.Inst.a_bleed);
	}

	void SpawnText(Color col, string amt)
	{
		//GameObject temp = Instantiate(GameDrops.Inst.textObject, transform.position, GameDrops.Inst.textObject.transform.rotation) as GameObject;

		//TextObject tempText = temp.GetComponent<TextObject>();

		//tempText.SetParams(col, amt);


	}

}
