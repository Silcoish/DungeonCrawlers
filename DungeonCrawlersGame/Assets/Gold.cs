using UnityEngine;
using System.Collections;

public class Gold : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			GameManager.inst.gameDataManager.GoldCollected(10);
			Destroy(gameObject);
		}
	}
}
