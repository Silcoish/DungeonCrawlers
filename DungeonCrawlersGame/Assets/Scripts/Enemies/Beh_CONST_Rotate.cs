using UnityEngine;
using System.Collections;

public class Beh_CONST_Rotate : Behaviour
{

	public float rotateSpeed;

	// Update is called once per frame
	public override void BehaviourUpdate(Enemy en)
	{
		en.RotateLookDirection(rotateSpeed * Time.deltaTime);
	}
}
