using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class DungeonLayoutLoader : MonoBehaviour
{

	public class RoomObject
	{
		GameObject room;
		GameObject doorNorth;
		GameObject doorSouth;
		GameObject doorEast;
		GameObject doorWest;
	}

	[Tooltip("If you set this value, it will use the specified layout instead of randomly choosing one")]
	public string fileName = "";
	public List<GameObject> templateRooms;
	GameObject[] rooms;
	public GameObject player;
	public GameObject doorNorth;
	public GameObject doorSouth;
	public GameObject doorEast;
	public GameObject doorWest;
	public Vector2 roomOffset = new Vector2(19.2f, 10.8f);
	[Tooltip("North Door, East Door, South Door, West Door")]
	public Vector4 doorOffset = new Vector4(4.08f, 9.16f, 4.95f, 8.95f);

	private int SIZE = 15;

	void Start()
	{
		rooms = new GameObject[SIZE * SIZE];

		print(rooms[0]);

		if(fileName == "")
			ChooseLayout();
		SetupLayout();
		PlaceDoors();
	}

	void PlaceDoors()
	{
		for(int i = 0; i < SIZE * SIZE; i++)
		{
			//check for room on the left
			if((i + 1) % SIZE != 0)
			{
				if (rooms[i] != null && rooms[i + 1] != null)
				{
					Instantiate(doorEast, (Vector2)rooms[i].transform.position + new Vector2(doorOffset[1], 0f), doorEast.transform.rotation);
					Instantiate(doorWest, (Vector2)rooms[i + 1].transform.position - new Vector2(doorOffset[3], 0f), doorWest.transform.rotation);
				}
			}

			//check for room on the bottom
			if(i < (SIZE * SIZE) - SIZE)
			{
				if(rooms[i] != null && rooms[i + SIZE] != null)
				{
					Instantiate(doorSouth, (Vector2)rooms[i].transform.position - new Vector2(0f, doorOffset[2]), doorSouth.transform.rotation);
					Instantiate(doorNorth, (Vector2)rooms[i].transform.position + new Vector2(0f, doorOffset[0]), doorNorth.transform.rotation);
				}
			}
		}
	}

	void ChooseLayout()
	{
		DirectoryInfo info = new DirectoryInfo(Application.dataPath + "/Layouts/");
		FileInfo[] fileInfo = info.GetFiles();
		List<string> fileNames = new List<string>();

		for (int i = 0; i < fileInfo.Length; i++)
		{
			if (!fileInfo[i].Name.Contains("meta"))
			{
				fileNames.Add(fileInfo[i].Name);
			}
		}

		fileName = fileNames[Random.Range(0, fileNames.Count)];
	}

	void SetupLayout()
	{
		string filePath = Application.dataPath + "/Layouts/" + fileName;

		string line;
		int lineNum = 0;
		StreamReader reader = new StreamReader(filePath);
		using (reader)
		{
			do
			{
				line = reader.ReadLine();

				if (line != null)
				{
					string[] entries = line.Split(',');
					if (entries.Length > 0)
					{
						for (int i = 0; i < entries.Length-1; i++)
						{
							if(entries[i] != "0" && entries[i] != " 0")
							{
								rooms[lineNum * SIZE + i] = (GameObject)Instantiate(templateRooms[Random.Range(0, templateRooms.Count)], new Vector2(i * roomOffset.x, -lineNum * roomOffset.y), Quaternion.identity);
							}

							if(entries[i] == "2" || entries[i] == " 2")
							{
								Instantiate(player, rooms[lineNum * SIZE + i].transform.position, Quaternion.identity);
							}
						}
					}
				}

				lineNum++;
			}
			while (line != null);

			reader.Close();
		}
	}


}
