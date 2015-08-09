using UnityEngine;
using System.Collections;

public class DungeonGeneration {

	DungeonSets.DungeonPieces set;

	public void LoadSet(DungeonSets.DungeonPieces s)
	{
		set = s;
	}

	public Dungeon.Room CreateRoom()
	{
		return new Dungeon.Room(set.room);
	}

}
