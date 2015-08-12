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
		bool isStartRoom = false;
		float xOffset = 19.2f;
		float yOffset = 10.8f;
		public GameObject room;
		GameObject enemiesParentNode;
		public List<GameObject> doors;
		List<GameObject> enemies;

		public Room(DungeonSets.DungeonPieces sett, bool startRoom)
		{
			set = sett;
			doors = new List<GameObject>();
			enemies = new List<GameObject>();
			isStartRoom = startRoom;
		}

		public void SpawnRoom(int xx, int yy)
		{
			x = xx;
			y = yy;
			room = (GameObject)Instantiate(set.room, new Vector2(x * 19.2f, -y * 10.8f), Quaternion.identity);
			enemiesParentNode = new GameObject();
			enemiesParentNode.transform.parent = room.transform;
			enemiesParentNode.name = "Enemies";
		}

		public void SpawnDoor(bool u, bool r, bool d, bool l)
		{
			if (u)
			{
				GameObject tempDoor = (GameObject)Instantiate(set.northDoor, new Vector2(x * xOffset, -y * yOffset + 4.09f), set.northDoor.transform.rotation);
				doors.Add(set.northDoor);
				//doors[doors.Count - 1].name = "Door_North";
				doors[doors.Count - 1].GetComponent<Door>().dir = Door.Direction.NORTH;
			}
			else if (r)
			{
				GameObject tempDoor = (GameObject)Instantiate(set.eastDoor, new Vector2(x * xOffset + 9.15f, -y * yOffset), set.eastDoor.transform.rotation);
				doors.Add(tempDoor);
				//doors[doors.Count - 1].name = "Door_East";
				doors[doors.Count - 1].GetComponent<Door>().dir = Door.Direction.EAST;
			}
			else if (d)
			{
				GameObject tempDoor = (GameObject)Instantiate(set.southDoor, new Vector2(x * xOffset, -y * yOffset - 4.92f), set.southDoor.transform.rotation);
				doors.Add(tempDoor);
				//doors[doors.Count - 1].name = "Door_South";
				doors[doors.Count - 1].GetComponent<Door>().dir = Door.Direction.SOUTH;
			}
			else if (l)
			{
				GameObject tempDoor = (GameObject)Instantiate(set.westDoor, new Vector2(x * xOffset - 8.95f, -y * yOffset), set.westDoor.transform.rotation);
				doors.Add(tempDoor);
				//doors[doors.Count - 1].name = "Door_West";
				doors[doors.Count - 1].GetComponent<Door>().dir = Door.Direction.WEST;
			}
	
		}

		public void SpawnEnemies()
		{
			if (!isStartRoom)
			{
				int amount = Random.Range(1, 7);
				for (int i = 0; i < amount; i++)
				{
					enemies.Add(set.enemies[Random.Range(0, set.enemies.Count)]);
					GameObject tempEnemy = (GameObject)Instantiate(enemies[i], new Vector2(x * xOffset, -y * yOffset), Quaternion.identity);
					tempEnemy.transform.parent = enemiesParentNode.transform;
				}
			}
			else
			{
				GameManager.inst.player = (GameObject)Instantiate(set.player, room.transform.position, Quaternion.identity);

				GameManager.inst.player.GetComponent<Player>().SetRoomData(x, y, room);
			}

			enemiesParentNode.SetActive(false);
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
		bool startRoomSet = false;
		for (int i = 0; i < roomAmount; i++)
		{
			dg = new DungeonGeneration();
			dg.LoadSet(GameManager.inst.dungeonSets.set[0]);
			if (Random.Range(i, roomAmount) == (roomAmount - 1) && !startRoomSet)
			{
				rooms.Add(dg.CreateRoom(true));
				startRoomSet = true;
			}
			else
				rooms.Add(dg.CreateRoom(false));
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

	public void SwitchRooms(Door.Direction dir)
	{
		int tempX, tempY;
		tempX = GameManager.inst.player.GetComponent<Player>().roomData.x;
		tempY = GameManager.inst.player.GetComponent<Player>().roomData.y;

		switch(dir)
		{
			case Door.Direction.NORTH:
				GameManager.inst.player.GetComponent<Player>().roomData.currentRoom = rooms[grid[(tempY - 1) * GRID_WIDTH + tempX]].room;
				GameManager.inst.player.GetComponent<Player>().roomData.y -= 1;
				tempY -= 1;
				break;
			case Door.Direction.EAST:
				GameManager.inst.player.GetComponent<Player>().roomData.currentRoom = rooms[grid[tempY * GRID_WIDTH + tempX + 1]].room;
				GameManager.inst.player.GetComponent<Player>().roomData.x += 1;
				tempX += 1;
				break;
			case Door.Direction.SOUTH:
				GameManager.inst.player.GetComponent<Player>().roomData.currentRoom = rooms[grid[(tempY + 1) * GRID_WIDTH + tempX]].room;
				GameManager.inst.player.GetComponent<Player>().roomData.y += 1;
				tempY += 1;
				break;
			case Door.Direction.WEST:
				GameManager.inst.player.GetComponent<Player>().roomData.currentRoom = rooms[grid[tempY* GRID_WIDTH + tempX - 1]].room;
				GameManager.inst.player.GetComponent<Player>().roomData.x -= 1;
				tempX -= 1;
				break;
		}

		int intOppDir = (int)dir + 2;
		if(intOppDir >= 4)
			intOppDir -= 4;

		Room tempRoom = rooms[grid[tempY * GRID_WIDTH + tempX]];
		print("Opposite door: " + ((Door.Direction)intOppDir) + ", next room: " + grid[tempY * GRID_WIDTH + tempX]);
		for(int i = 0; i < tempRoom.doors.Count; i++)
		{
			if ((int)tempRoom.doors[i].GetComponent<Door>().dir == intOppDir)
			{
				print("hit this weird place");
				GameManager.inst.player.transform.position = tempRoom.doors[i].transform.GetChild(0).transform.position;
			}
		}
	}

}
