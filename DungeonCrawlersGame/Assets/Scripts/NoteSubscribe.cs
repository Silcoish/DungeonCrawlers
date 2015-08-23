using UnityEngine;
using System.Collections;

public class NoteSubscribe : MonoBehaviour {

	public int note;
	public int velocity;
	public int channel = 1;

	public enum State
	{
		ACTIVE,
		DEACTIVE
	}

	public State state = State.DEACTIVE;
	private bool isSubbed = false;

	void Start()
	{
		Sub();
	}

	public void Sub()
	{
		if(!isSubbed)
		{
			GameManager.inst.MidiSubscribe(this);
			isSubbed = true;
		}
	}

	public void Activate()
	{
		state = State.ACTIVE;
	}

	public void Deactivate()
	{
		state = State.DEACTIVE;
	}
}
