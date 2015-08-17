using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class QuestGiver : MonoBehaviour 
{
	public bool isSelected = false;

	[SerializeField]
	public Quest quest;
	public GameObject prompt;
	public GameObject questText;
	public Text textQuest;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			prompt.SetActive(true);
			questText.SetActive(false);

			textQuest.text = quest.GetText();
		}
	}

	void OnTriggerStay2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			if (Input.GetButtonDown("Submit"))
			{
				print("2");

				if (isSelected)
				{
					print("4");
					prompt.SetActive(false);
					questText.SetActive(false);
					isSelected = false;

					GameManager.inst.questManager.currentQuest = quest;
				}
				else
				{
					prompt.SetActive(false);
					questText.SetActive(true);

					isSelected = true;
				}
			}
		}

	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.tag == "Player")
		{
			prompt.SetActive(false);
			questText.SetActive(false);
			isSelected = false;
		}

	}
}
