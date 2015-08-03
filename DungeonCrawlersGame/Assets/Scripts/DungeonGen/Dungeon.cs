using UnityEngine;
using System.Collections;

public class Dungeon : MonoBehaviour{

	public class Room
	{
		GameObject roomGameobject;
		int x, y;

		public Room(int xx, int yy, GameObject go)
		{
			int x = xx;
			int y = yy;
			roomGameobject = go;
			Instantiate(go, new Vector2(x, y), Quaternion.identity);
		}
	};

	public int roomAmount = 1;
	Room[] rooms;

	DungeonGeneration dg;
	 
	void Start()
	{
		dg = new DungeonGeneration();
		dg.LoadSet(GameManager.inst.dungeonSets.set[0]);
		dg.CreateRoom();
	}

}
