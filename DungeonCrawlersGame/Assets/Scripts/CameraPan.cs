﻿using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour 
{
	Camera cam;

	public float minheight;
	public float maxheight;
	public float lerp;

	public Vector3 firstPos;
	public Vector3 secondPos;

	public float firstSize;
	public float secondSize;

	void Awake()
	{
		cam = Camera.main;
		minheight = gameObject.transform.position.y - (gameObject.transform.localScale.y / 2);
		maxheight = gameObject.transform.position.y +(gameObject.transform.localScale.y / 2);
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			lerp = (col.gameObject.transform.position.y - minheight) / (maxheight - minheight);

			lerp = Mathf.Clamp(lerp, 0, 1);

			cam.transform.localPosition = Vector3.Lerp(firstPos, secondPos, lerp);
			cam.orthographicSize = Mathf.Lerp(firstSize, secondSize, lerp);


		}



	}
}
