using UnityEngine;
using System.Collections;

//Please kill this script after w13 prototype.
//do it properly. 

public class HackyHacky1 : MonoBehaviour {

	void Start () {
		GameManager.inst.midiSystem.ResetMidi();
	}
}
