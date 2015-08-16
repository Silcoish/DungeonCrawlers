using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis("Start") > 0.5)
		{
			Application.LoadLevel(1);
		}
	
	}
}
