using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour {

	void Update () {
		transform.position = new Vector3(0, 0, transform.position.z) + (Vector3)GameManager.inst.player.GetComponent<Player>().roomData.currentRoom.transform.position;
	}
}
