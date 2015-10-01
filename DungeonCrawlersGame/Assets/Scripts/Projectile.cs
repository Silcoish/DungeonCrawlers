using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public int dmg;
    public float speed;
    public int kb;
    public float lifetime;
    public StatusEffect effect;

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
}
