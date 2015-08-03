using UnityEngine;
using System.Collections;


public class Beh_CONST_MoveForward : Behaviour 
{
	public float moveSpeed;


	// Update is called once per frame
	public override void BehaviourUpdate(Enemy en) 
	{
		en.SetSpeed(moveSpeed);
	}
}
