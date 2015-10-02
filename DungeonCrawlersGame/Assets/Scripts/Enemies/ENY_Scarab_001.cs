using UnityEngine;
using System.Collections;

public class ENY_Scarab_001 : Enemy 
{

	public float speed = 1;
	public int numberOfDirections = 4;

	float timer = 0;
	public float turnTime = 1;

	Vector2 direction = Vector2.up;
	// Use this for initialization
	void Start () 
	{
		direction = RandDirection();
	}
	
	// Update is called once per frame
	void Update () 
	{
		rb.velocity = (direction * speed * globalMoveSpeed);


		timer += Time.deltaTime;

		if (timer > turnTime)
		{
			timer = 0;
			direction = RandDirection();
		}

	}

	Vector2 RandDirection()
	{
		Vector2 temp = Vector2.up;
		float randAmount = Random.Range(0, numberOfDirections) * (360 / numberOfDirections);
		return Quaternion.Euler(0, 0, randAmount) * temp;
	}
}
