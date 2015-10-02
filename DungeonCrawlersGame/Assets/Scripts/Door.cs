using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public enum Direction
	{
		SOUTH,
		EAST,
        NORTH,
		WEST
	}

    public Transform partnerDoor;
    public Transform parentRoom;

	public Direction dir;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag == "Player")
		{
            col.transform.position = partnerDoor.GetChild(0).position;
            print(partnerDoor.gameObject.GetComponent<Door>());
            col.GetComponent<Player>().currentRoom = partnerDoor.gameObject.GetComponent<Door>().parentRoom;
		}
	}
}
