using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextObject : MonoBehaviour 
{
	public float moveSpeed = 10;
	public float liveTime = 0.5f;

	Vector2 dir = Vector2.zero;

	float timer = 0;

	public Text dispText;
	// Use this for initialization
	void Awake () 
	{
		dispText = gameObject.GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(dir * moveSpeed * Time.deltaTime);

		timer += Time.deltaTime;

		if (timer >= liveTime)
			Destroy(gameObject);
		
	}

	public void SetParams(Color col, string txt)
	{
		dispText.color = col;
		dispText.text = txt;
		dir = Random.insideUnitCircle.normalized;
	}

	public void SetParams(Color col, string txt, Vector2 moveDir)
	{
		dispText.color = col;
		dispText.text = txt;
		dir = moveDir;

	}
}
