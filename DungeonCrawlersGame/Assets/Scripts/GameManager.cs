using UnityEngine;
using System.Collections;

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

	void Awake()
	{
		if (GameManager.inst == null)
			GameManager.inst = this;
		else
			Destroy(gameObject);
	}

	public void MidiSubscribe(NoteSubscribe sub)
	{
		midiSystem.Subscribe(sub);
	}
}
