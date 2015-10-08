using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

    int goldValue = 10;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
            GameManager.inst.inventory.gold += goldValue;
			AudioManager.Inst.PlaySFX(AudioManager.Inst.a_coin);
			Destroy(gameObject);
		}
	}
}
