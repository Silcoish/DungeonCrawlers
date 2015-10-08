using UnityEngine;
using System.Collections;

public class GameDrops : MonoBehaviour 
{
	public static GameDrops Inst;

	public GameObject textObject;

	void Awake()
	{
		if (Inst == null)
		{
			Inst = this;
		}
	}

}
