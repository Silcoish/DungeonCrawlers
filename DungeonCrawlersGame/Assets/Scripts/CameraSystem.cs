using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour {

    public bool isEnabled = true;
	Transform t;
	Player p;
	float camSpeed = 10f;

	public bool destination = false;

	void Start()
	{
		t = Camera.main.transform;
		p = GameManager.inst.player.GetComponent<Player>();
	}

	void Update () {
		if (p.currentRoom != null)
		{
			if (!destination)
			{
				t.position = Vector3.Lerp(t.position, new Vector3(p.currentRoom.position.x, p.currentRoom.position.y, t.position.z), 1 / camSpeed);
				if (Vector2.Distance(t.position, p.currentRoom.position) < 0.1f)
					destination = true;
			}
			else
			{
				t.position = new Vector3(p.currentRoom.position.x, p.currentRoom.position.y, t.position.z);
			}
		}	
	}

	public void MoveRoom(Transform newTarget)
	{
		GameManager.inst.player.GetComponent<Player>().currentRoom = newTarget;
		destination = false;
	}
}
