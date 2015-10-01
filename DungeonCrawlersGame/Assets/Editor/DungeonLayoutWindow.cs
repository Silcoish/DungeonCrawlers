using UnityEngine;
using UnityEditor;
using System.IO;

public class DungeonLayoutWindow : EditorWindow
{
    [System.Serializable]
    public class RoomElement
    {
        public enum States
        {
            EMPTY,
            ROOM,
            STARTROOM,
            BOSSROOM
        }
        public States state = States.EMPTY;

        [SerializeField] public Color color = Color.white;

        public void NextState()
        {
            if (state == States.EMPTY)
                SwitchState(States.ROOM);
            else if (state == States.ROOM)
                SwitchState(States.STARTROOM);
            else if (state == States.STARTROOM)
                SwitchState(States.BOSSROOM);
            else
                SwitchState(States.EMPTY);
        }

        public void SwitchState(States newState)
        {
            switch (newState)
            {
                case States.EMPTY:
                    state = States.EMPTY;
                    color = Color.white;
                    break;
                case States.ROOM:
                    state = States.ROOM;
                    color = Color.blue;
                    break;
                case States.STARTROOM:
                    state = States.STARTROOM;
                    color = Color.green;
                    break;
                case States.BOSSROOM:
                    state = States.BOSSROOM;
                    color = Color.red;
                    break;
                default:
                    state = States.EMPTY;
                    color = Color.white;
                    break;
            }
        }

    };

	private static int SIZE = 15;
	[SerializeField] public RoomElement[] layout = new RoomElement[SIZE * SIZE];
	public int index = 0;
	public int lastIndex = 0;
	public string selectedFileName;
	public string newFileName;
	public bool showFileNameError = false;

	[MenuItem("Window/DungeonLayoutWindow")]
	static void Init()
	{
		DungeonLayoutWindow window = (DungeonLayoutWindow)EditorWindow.GetWindow(typeof(DungeonLayoutWindow));
        for (int i = 0; i < SIZE * SIZE; i++)
        {
            window.layout[i] = new RoomElement();
        }
		window.Show();
	}

	void OnGUI()
	{
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Layout Editor");
		EditorGUILayout.Space();

		ShowFileNames();

		EditorGUILayout.Space();

		if(index != 0 && lastIndex != index)
		{
			LoadData();
		}

		if(index == 0)
		{
			EditorGUILayout.LabelField("Enter Layout File Name");
			newFileName = EditorGUILayout.TextField(newFileName);
		}

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("White = Empty");
        EditorGUILayout.LabelField("Blue = Room");
        EditorGUILayout.LabelField("Green = Start Room");
        EditorGUILayout.LabelField("Red = Boss Room");
		for (int i = 0; i < SIZE; i++)
		{
			Rect r = EditorGUILayout.BeginHorizontal("Button");
			for (int j = 0; j < SIZE; j++)
			{
                GUI.color = layout[i * SIZE + j].color;
                if(GUILayout.Button(""))
                {
                    layout[i * SIZE + j].NextState();
                }
                GUI.color = Color.white;
			}
			EditorGUILayout.EndHorizontal();
		}

		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		if(GUILayout.Button("Save"))
		{
			SaveLayout();	
		}
		if(GUILayout.Button("Clear"))
		{
			if(EditorUtility.DisplayDialog("Clear Layout", "Are you sure you want to clear the current layout? Remember: You will need to save to keep changes", "Yes", "No"))
			{
				for(int i = 0; i < layout.Length; i++)
				{
                    layout[i].SwitchState(RoomElement.States.EMPTY);
				}
			}
		}
        if(GUILayout.Button("Randomise"))
        {
            if(EditorUtility.DisplayDialog("Randomise Rooms", "Are you sure you wish to clear all rooms and place random ones?", "Yes", "No"))
            {
                for(int i = 0; i < layout.Length; i++)
                {
                    layout[i].SwitchState((RoomElement.States)Random.Range(0, 2));
                }
            }
        }
		EditorGUILayout.EndHorizontal();

		if(showFileNameError)
		{
			EditorGUILayout.HelpBox("File name is invalid or already taken", MessageType.Error);
		}
	}

	void ShowFileNames()
	{
		FileInfo[] fileInfo = GetFileInfo();
		string[] fileNames = new string[fileInfo.Length + 1];
		fileNames[0] = "New";
		for(int i = 1; i < fileNames.Length; i++)
		{
			fileNames[i] = fileInfo[i - 1].Name;
		}
		lastIndex = index;
		index = EditorGUILayout.Popup(index, fileNames);
		selectedFileName = fileNames[index];
	}

	FileInfo[] GetFileInfo()
	{
		DirectoryInfo info = new DirectoryInfo(Application.dataPath + "/Layouts/");
		FileInfo[] fileInfo = info.GetFiles();
		return fileInfo;
	}

	void LoadData()
	{
		string filePath = Application.dataPath + "/Layouts/" + selectedFileName;

		string line;
		int lineNum = 0;
		StreamReader reader = new StreamReader(filePath);
		using(reader)
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
                            if (entries[i] == "3" || entries[i] == " 3")
                                layout[lineNum * SIZE + i].SwitchState((RoomElement.States)3);
                            else if (entries[i] == "2" || entries[i] == " 2")
                                layout[lineNum * SIZE + i].SwitchState((RoomElement.States)2);
                            else if (entries[i] == "1" || entries[i] == " 1")
                                layout[lineNum * SIZE + i].SwitchState((RoomElement.States)1);
                            else
                                layout[lineNum * SIZE + i].SwitchState((RoomElement.States)0);
						}
					}
				}

				lineNum++;
			}
			while (line != null);
			
			reader.Close();
			
		}
	}

	void SaveLayout()
	{
		string path = Application.dataPath + "/Layouts/";
        
		if(index == 0)
		{
			if(newFileName != "" && !File.Exists(path + "" + newFileName + ".csv"))
			{
				string output = CreateSaveString();
				File.WriteAllText(path + "" + newFileName + ".csv", output);
				showFileNameError = false;
			}
			else
			{
				showFileNameError = true;
			}
		}
		else
		{
			string output = CreateSaveString();
			File.WriteAllText(path + "" + selectedFileName, output);
		}
		Debug.Log(newFileName);
	}

	string CreateSaveString()
	{
		string retval = "";
		for(int i = 0; i < SIZE; i++)
		{
			for(int j = 0; j < SIZE; j++)
			{
				retval += ((int)layout[i * SIZE + j].state).ToString();

				if (i == SIZE - 1 && j == SIZE - 1)
				{ }
				else
					retval += ", ";
			}
			if (i != (SIZE - 1))
				retval += "\n";
		}

		return retval;
	}

    bool LayoutContainsStart(RoomElement.States roomToCheck)
    {
        int roomCount = 0;
        for (int i = 0; i < layout.Length; i++)
        {
            if (layout[i].state == roomToCheck)
            {
                roomCount++;
            }
        }

        if (roomCount == 1)
            return true;

        return false;
    }

}