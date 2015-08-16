using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public enum Direction
	{
		SOUTH,
		EAST,
        NORTH,
		WEST
	}

	public Direction dir;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
			GameManager.inst.dungeon.SwitchRooms(dir);
		}
	}
}
