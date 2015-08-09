using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoBehaviour{

	public const int GRID_WIDTH  = 10;
	public const int GRID_HEIGHT = 10;


	[System.Serializable]
	public class Room
	{
		GameObject roomGameobject;
		int x, y;

		public Room(GameObject go)
		{
			roomGameobject = go;
			//Instantiate(go, new Vector2(x, y), Quaternion.identity);
		}

		public void SpawnRoom(int xx, int yy)
		{
			int x = xx;
			int y = yy;
			Instantiate(roomGameobject, new Vector2(xx * 19.2f, y), Quaternion.identity);
		}
	};

	public int roomAmount;
	[SerializeField] List<Room> rooms;
	public int[] grid;

	DungeonGeneration dg;

	public bool shift = false;
	 
	void Start()
	{
		grid = new int[GRID_WIDTH * GRID_HEIGHT];
		ResetRooms();

		CreateRooms();
	}

	void Update()
	{
		if(shift)
		{
			ShiftRoomsRight();
			shift = false;
			grid[GRID_WIDTH * GRID_HEIGHT - 9] = 1000;
			grid[GRID_WIDTH * GRID_HEIGHT - 1] = 1001;
			ShiftRoomsDown();
		}
	}

	void CreateRooms()
	{
		for (int i = 0; i < roomAmount; i++)
		{
			dg = new DungeonGeneration();
			dg.LoadSet(GameManager.inst.dungeonSets.set[0]);
			rooms.Add(dg.CreateRoom());
			grid[0] = i;
			ShiftRoomsRight();
		}
		SpawnRooms();
	}

	void SpawnRooms()
	{
		for(int i = 0; i < grid.Length; i++)
		{
			if(grid[i] != -1)
			{
				rooms[grid[i]].SpawnRoom(i % GRID_WIDTH, (int)Mathf.Floor(i / GRID_HEIGHT));
			}
		}
	}

	void ResetRooms()
	{
		for(int i = 0; i < grid.Length; i++)
		{
			grid[i] = -1;
		}

		rooms.Clear();
	}

	void ShiftRoomsRight()
	{
		
		for(int i = 0; i < GRID_HEIGHT; i++)
		{
			int last = grid[(i + 1) * GRID_WIDTH - 1];
			for (int index = GRID_WIDTH - 2; index >= 0; index--)
				grid[i * GRID_WIDTH + index + 1] = grid[i * GRID_WIDTH + index];
			grid[i * GRID_WIDTH] = last;
		}
	}

	void ShiftRoomsDown()
	{
		int[] last = new int[GRID_WIDTH];
		for(int i = 0; i < GRID_HEIGHT; i++)
		{
			for (int j = 0; j < GRID_WIDTH; j++)
				last[i] = grid[GRID_WIDTH * GRID_HEIGHT - (GRID_WIDTH - j)];
		}
	}

}
