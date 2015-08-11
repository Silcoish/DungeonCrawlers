using UnityEngine;
using System.Collections;

public class DungeonGeneration {

	DungeonSets.DungeonPieces set;

	[System.Flags]
	enum Options
	{
		UP		= (1 << 0),
		RIGHT	= (1 << 1),
		DOWN	= (1 << 2),
		LEFT	= (1 << 3)
	}

	public enum Direction
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public void LoadSet(DungeonSets.DungeonPieces s)
	{
		set = s;
	}

	public Dungeon.Room CreateRoom()
	{
		return new Dungeon.Room(set);
	}

	/*public void CreateDoor(Dungeon.Room room,  options)
	{
		if ((options & Options.UP) == Options.UP)
			Debug.Log("UP");
		if (options & Options.RIGHT)
			Debug.Log("RIGHT");
		if (options & Options.DOWN)
			Debug.Log("DOWN");
		if (options & Options.LEFT)
			Debug.Log("LEFT");
	}*/

}
