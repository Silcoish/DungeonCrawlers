using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour 
{
	public int damage = 0;
	public float knockbackForce = 1;

    public DamageType effect;
    public float effectDuration;
    public float effectStrength;

	void OnCollisionEnter2D(Collision2D col)
	{
		print("Colision");
		if (col.collider.tag == "Wall")
		{
			Destroy(gameObject);
		}
		else if (col.collider.tag == "Player")
		{
			//TODO Link up damage from prefab input
			col.gameObject.GetComponent<Player>().OnTakeDamage(GetDamage());
			Destroy(gameObject);
		}
		else if (col.collider.tag == "Player" || col.collider.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy>().OnTakeDamage(GetDamage());
			Destroy(gameObject);
		}

	}

    public Damage GetDamage()
    {
        Damage temp;

        temp.type = effect;
        temp.amount = damage;
        temp.knockback = knockbackForce;
        temp.fromGO = gameObject.transform;
        temp.effectTime = effectDuration;
        temp.effectStrength = effectStrength;

        return temp;
    }
}
