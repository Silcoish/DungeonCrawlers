using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour {

	Transform t;
	Player p;
	float camSpeed = 10f;

	void Start()
	{
		t = transform;
		p = GameManager.inst.player.GetComponent<Player>();
	}

	void Update () {
		t.position = Vector3.Lerp(t.position, new Vector3(0, 0, t.position.z) + p.roomData.currentRoom.transform.position, 1/camSpeed);
			//new Vector3(0, 0, transform.position.z) + (Vector3)GameManager.inst.player.GetComponent<Player>().roomData.currentRoom.transform.position;
	}
}
