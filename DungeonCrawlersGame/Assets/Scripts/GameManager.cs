using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager inst;
	public MidiSystem midiSystem;
	public Dungeon dungeon;
	public DungeonSets dungeonSets;
    public Stats stats;
    public Inventory inventory;
    public ActiveItems activeItems;
    public VendorInventory vendorInventory;
	public GameObject player;
	public GameDataManager gameDataManager;
	public QuestManager questManager;
	public Dictionary<string, GameObject> enemyMap;

	void Awake()
	{
		if (GameManager.inst == null)
			GameManager.inst = this;
		else
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	public void MidiSubscribe(NoteSubscribe sub)
	{
		midiSystem.Subscribe(sub);
	}
}
