using UnityEngine;
using System.Collections;

public class Beh_DAM_ForceBack : Behaviour 
{
	bool isKnockBack;
	bool firstHit = false;
	Vector2 Dir;

	[Range(1,50)]
	public float knockbackForce;
	public float knockbackTime;

	float timer;
	
	public override void BehaviourStart()
	{
		isKnockBack = false;
	}
	public override void BehaviourUpdate(Enemy en)
	{
		if (isKnockBack)
		{
			timer += Time.deltaTime;
			if (timer > knockbackTime)
				isKnockBack = false;
			en.SetMoveDirection(Dir);

			if (firstHit)
			{
				en.SetSpeed(knockbackForce);
				firstHit = false;
			}
			else
			{
				en.ResetSpeed();
			}
		}
	}

	public override void OnTakeDamage(int dam, Vector2 knockbackForce)
	{
		isKnockBack = true;
		Dir = knockbackForce;
		timer = 0;
		firstHit = true;
	}

	public override void OnDeath()
	{

	}

}
