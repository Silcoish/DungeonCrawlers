using UnityEngine;
using System.Collections;

public class Beh_DAM_ForceBack : Behaviour 
{
	bool isKnockBack;
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
			en.ResetSpeed();
			en.SetMoveDirection(Dir);
		}
	}

	public override void OnTakeDamage(Vector2 origin, int dam)
	{
		isKnockBack = true;
		Dir = ((Vector2)gameObject.transform.position - origin).normalized;
		timer = 0;
	}

	public override void OnDeath()
	{

	}

}
