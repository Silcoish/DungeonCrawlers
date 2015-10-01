using UnityEngine;
using System.Collections;

public class TargetDummy : MonoBehaviour 
{
    public int damage = 0;
    public int knockback = 5;
    public DamageType effect;
    public float effectDuration;
    public float effectStrength;

    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

	void OnTakeDamage(int dmg, Vector2 kb)
    {
        // Does not have hp to be reduced.

        rb2D.AddForce(kb, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {     
        if(col.gameObject.tag == "Player")
        {
			Vector2 kbForce = -gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
			col.gameObject.GetComponent<Player>().OnTakeDamage(GetDamage());
        }
        if(col.gameObject.tag == "Enemy")
        {
			Vector2 kbForce = -gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
			col.gameObject.GetComponent<Enemy>().OnTakeDamage(GetDamage());
        }

        // If we standardize the damage call
        //col.gameObject.GetComponent<Damageable>().TakeDamage(damage, transform.position, knockback);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //if (col.gameObject.tag == "WepLeft")
        //{
        //    Weapon wep = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>();
        //    Vector2 kbForce = col.gameObject.GetComponentInParent<Rigidbody2D>().velocity.normalized;
        //    OnTakeDamage(wep.dmg, kbForce * wep.kb);
        //}
        if (col.gameObject.tag == "WepRight")
        {
            Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();
            Vector2 kbForce = col.gameObject.GetComponentInParent<Rigidbody2D>().velocity.normalized;
            OnTakeDamage(wep.dmg, kbForce * wep.kb);
        }
    }

    //void OnTriggerEnter2D(Collider2D col)
    //{
    //    if(col.gameObject.tag == "WepLeft")
    //    {
    //        Weapon wep = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>();
    //        TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
    //    }
    //    if(col.gameObject.tag == "WepRight")
    //    {
    //        Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();
    //        TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
    //    }
    //}

    public Damage GetDamage()
    {
        Damage temp;

        temp.type = effect;
        temp.amount = damage;
        temp.knockback = knockback;
        temp.fromGO = gameObject.transform;
        temp.effectTime = effectDuration;
        temp.effectStrength = effectStrength;

        return temp;
    }
}
