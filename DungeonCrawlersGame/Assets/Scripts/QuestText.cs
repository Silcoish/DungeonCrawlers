using UnityEngine;
using System.Collections;

public class QuestText : MonoBehaviour {

	float speed = 2f;
	float fadeSpeed = 1f;
	TextMesh tm;

	void Start()
	{
		tm = GetComponent<TextMesh>();	
	}

	void Update()
	{
		transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
		tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, tm.color.a - fadeSpeed * Time.deltaTime);

		if (tm.color.a <= 0)
			Destroy(gameObject);
	}
}
