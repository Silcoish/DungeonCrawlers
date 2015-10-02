using UnityEngine;
using System.Collections;

public class CameraSystem : MonoBehaviour {

    public bool isEnabled = true;
	Transform t;
	Player p;
	float camSpeed = 10f;

	void Start()
	{
		t = Camera.main.transform;
		p = GameManager.inst.player.GetComponent<Player>();
	}

	void Update () {
        if(p.currentRoom != null)
            t.position = Vector3.Lerp(t.position, new Vector3(p.currentRoom.position.x, p.currentRoom.position.y, t.position.z), 1 / camSpeed);
	}
}
