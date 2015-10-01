using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int dmg;
    public float speed;
    public int kb;
    public float lifetime;
    public DamageType effect;
    public float effectDuration;
    public float effectStrength;

    public Vector2 direction;

	void Start () 
    {
        Destroy(gameObject, lifetime);
	}

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir, Vector3 rot)
    {
        direction = dir;
        transform.eulerAngles = rot;
    }

    public Damage GetDamage()
    {
        Damage temp;

        temp.type = effect;
        temp.amount = dmg;
        temp.knockback = kb;
        temp.fromGO = gameObject.transform;
        temp.effectTime = effectDuration;
        temp.effectStrength = effectStrength;

        return temp;
    }
}
