using UnityEngine;
using System.Collections;

public class DebugCubeLightUp : MonoBehaviour {

	NoteSubscribe sub;
	public Color startCol, endCol;
	Material mat;
	bool holdThisFrame = false;

	void Start()
	{
		mat = GetComponent<Renderer>().material;
		sub = GetComponent<NoteSubscribe>();
	}

	void Update()
	{
		if (sub.state == NoteSubscribe.State.ACTIVE)

			mat.color = endCol;
		else
			mat.color = startCol;
	}
}
