using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonSets : MonoBehaviour {

	[System.Serializable]
	public class DungeonPieces
	{
		[SerializeField] public GameObject room;
		[SerializeField] public GameObject northDoor;
		[SerializeField] public GameObject eastDoor;
		[SerializeField] public GameObject southDoor;
		[SerializeField] public GameObject westDoor;
	};

	[SerializeField] public List<DungeonPieces> set;
}
