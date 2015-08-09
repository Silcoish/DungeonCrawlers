using UnityEngine;
using System.Collections;

public class Beh_SPEC_RandomDirection : Behaviour
{
	public NoteSubscribe sub;

	NoteSubscribe.State prevState;

	public float movespeed;
	public int directions;
	[Range(0,15)]
	public float wallDistanceCheck;

	/// <summary>
	/// Only activate every "stateSkips" instance of the note. 
	/// ie. == 4. Only do every 4 beats.
	/// </summary>
	[Range(1,4)]
	public int stateSkips = 1;
	int skipCount = 0;



	// Update is called once per frame
	public override void BehaviourUpdate(Enemy en)
	{
		if (sub.state == NoteSubscribe.State.ACTIVE && prevState == NoteSubscribe.State.DEACTIVE)
		{
			skipCount++;
		}
		if (skipCount >= stateSkips)
		{
			skipCount = 0;
			int count = 0;
			//random dir
			do
			{
				count++;
				if (count > 100)
					break;
				en.SetMoveDirection(Random.Range(0, directions) * (360 / directions));
				en.SetSpeed(movespeed);
				Debug.DrawRay(transform.position, en.GetMoveDirection() * wallDistanceCheck, Color.red, 0.5f);
			} while (WallInFrontCheck(en.GetMoveDirection()));
			Debug.DrawRay(transform.position, en.GetMoveDirection() * wallDistanceCheck, Color.green, 0.5f);
		}

		prevState = sub.state;
	}


	bool WallInFrontCheck(Vector2 dir)
	{
		RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position,dir , wallDistanceCheck);

		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].collider.tag == "Wall")
			{
				//print("wall");
				return true;
			}
		}
		return false;
	}
}
