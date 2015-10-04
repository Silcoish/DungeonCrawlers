using UnityEngine;
using System.Collections;

public class Projectile : Damageable 
{
    public float speed;
    public float lifetime;

    public Vector2 direction;

	void Start () 
    {
        Destroy(gameObject, lifetime);
	}

    public override void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void SetDirection(Vector2 dir, Vector3 rot)
    {
        direction = dir;
        transform.eulerAngles = rot;
    }
}
