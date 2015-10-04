using UnityEngine;
using System.Collections;

public class TargetDummy : Damageable
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player" || col.gameObject.tag == "Enemy")
        {
			col.gameObject.GetComponent<Damageable>().OnTakeDamage(GetDamage());
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Weapon")
        {
            //Weapon wep = GameManager.inst.activeItems.wepSlot1.GetComponent<Weapon>();
            //OnTakeDamage(wep.dmg, kbForce * wep.kb);
        }
    }
}
