using UnityEngine;
using System.Collections;

public class ENY_Scorpion_001 : Enemy 
{
	[Header("EnemyAI")]
	public float speed = 1;

	Transform playerTrans;
	// Use this for initialization
	void Start () 
	{
		playerTrans = GameManager.inst.player.transform;
	}
	
	// Update is called once per frame
	public override void UpdateOverride() 
	{
		if (timerFreeze <= 0)
		{
			rb.velocity = ((playerTrans.position - transform.position).normalized * speed * globalMoveSpeed);

			transform.rotation = Quaternion.FromToRotation(Vector2.up, (playerTrans.position - transform.position));
		}
	}

}
