using UnityEngine;
using System.Collections;

public class TargetDummy : MonoBehaviour 
{
    public int damage = 0;
    public int knockback = 5;
    
    private Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

	void TakeDamage(int dmg, Vector2 pos, int kb)
    {
        // Does not have hp to be reduced.

        rb2D.AddForce(((Vector2)transform.position - pos) * kb, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {     
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().TakeDamage(damage, transform.position, knockback);
        }
        if(col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<Enemy>().OnTakeDamage(transform.position, damage);
        }

        // If we standardize the damage call
        //col.gameObject.GetComponent<Damageable>().TakeDamage(damage, transform.position, knockback);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "WepLeft")
        {
            Weapon wep = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>();
            TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
        }
        if(col.gameObject.tag == "WepRight")
        {
            Weapon wep = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>();
            TakeDamage(wep.dmg /* * stat.str*/, col.transform.position, wep.kb/* * stat.str?*/);
        }
    }
}
