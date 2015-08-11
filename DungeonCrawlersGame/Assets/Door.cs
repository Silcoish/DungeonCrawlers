using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public enum Direction
	{
		NORTH,
		EAST,
		SOUTH,
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
