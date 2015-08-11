using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dungeon : MonoBehaviour{

	public const int GRID_WIDTH  = 10;
	public const int GRID_HEIGHT = 10;


	[System.Serializable]
	public class Room
	{
		DungeonSets.DungeonPieces set;
		int x, y;
		float xOffset = 19.2f;
		float yOffset = 10.8f;
		List<GameObject> doors;
		List<GameObject> enemies;

		public Room(DungeonSets.DungeonPieces sett)
		{
			set = sett;
		}

		public void SpawnRoom(int xx, int yy)
		{
			x = xx;
			y = yy;
			print(x);
			print(y);
			Instantiate(set.room, new Vector2(x * 19.2f, -y * 10.8f), Quaternion.identity);
		}

		public void SpawnDoor(bool u, bool r, bool d, bool l)
		{
			print(x);
			print(y);
			if (u)
				Instantiate(set.northDoor, new Vector2(x * xOffset, -y * yOffset + 4.09f), set.northDoor.transform.rotation);
			else if (r)
				Instantiate(set.eastDoor, new Vector2(x * xOffset + 9.15f, -y * yOffset), set.eastDoor.transform.rotation);
			else if (d)
				Instantiate(set.southDoor, new Vector2(x * xOffset, -y * yOffset - 4.92f), set.southDoor.transform.rotation);
			else if (l)
				Instantiate(set.westDoor, new Vector2(x * xOffset - 8.95f, -y * yOffset), set.westDoor.transform.rotation);
		}

		public void SpawnEnemies()
		{
			int amount = Random.Range(1, 7);
			for(int i = 0; i < amount; i++)
			{
				Instantiate(set.enemies[Random.Range(0, set.enemies.Count)], new Vector2(x * xOffset, y * yOffset), Quaternion.identity);
			}
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

		SpawnDoors();
	}

	void SpawnDoors()
	{
		for(int i = 0; i < grid.Length; i++)
		{
			if(grid[i] != -1)
			{
				if (grid[i + 1] != -1)
				{
					rooms[grid[i]].SpawnDoor(false, true, false, false);//.CreateDoor(rooms[grid[i]], DungeonGeneration.Direction.RIGHT);
					rooms[grid[i + 1]].SpawnDoor(false, false, false, true); //dg.CreateDoor(rooms[grid[i + 1]], DungeonGeneration.Direction.LEFT);
				}
				if(grid[i + GRID_WIDTH] != -1)
				{
					rooms[grid[i]].SpawnDoor(false, false, true, false);//.CreateDoor(rooms[grid[i]], DungeonGeneration.Direction.RIGHT);
					rooms[grid[i + GRID_WIDTH]].SpawnDoor(true, false, false, false); //dg.CreateDoor(rooms[grid[i + 1]], DungeonGeneration.Direction.LEFT);
				}
			}
		}

		SpawnEnemies();
	}

	void SpawnEnemies()
	{
		for (int i = 0; i < grid.Length; i++)
		{
			if (grid[i] != -1)
			{
				rooms[grid[i]].SpawnEnemies();
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
