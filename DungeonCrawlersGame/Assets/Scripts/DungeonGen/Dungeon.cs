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
			Instantiate(roomGameobject, new Vector2(x * 19.2f, -y * 10.8f), Quaternion.identity);
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

	void CreateRooms()
	{
		for (int i = 0; i < roomAmount; i++)
		{
			dg = new DungeonGeneration();
			dg.LoadSet(GameManager.inst.dungeonSets.set[0]);
			rooms.Add(dg.CreateRoom());
			if (Random.Range(0, 2) == 0)
				ShiftRoomsDown();
			else
				ShiftRoomsRight();
			grid[0] = i;
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
		for (int j = 0; j < GRID_WIDTH; j++)
			last[j] = grid[GRID_WIDTH * GRID_HEIGHT - 1 - (GRID_WIDTH - 1 - j)];
		
		for(int i = GRID_HEIGHT - 2; i >= 0; i--)
		{
			for (int k = 0; k < GRID_WIDTH; k++)
			{
				grid[(i + 1) * GRID_WIDTH + k] = grid[i * GRID_WIDTH + k];
			}
		}

		for(int c = 0; c < GRID_WIDTH; c++)
		{
			grid[c] = last[c];
		}
	}

}
