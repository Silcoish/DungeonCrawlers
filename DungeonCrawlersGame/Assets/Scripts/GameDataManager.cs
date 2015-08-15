using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameDataManager : MonoBehaviour 
{

	List<Enemy> allEnemiesKilled = new List<Enemy>();

	int roomsComplete = 0;

	int goldCollected = 0;


	public void ResetData()
	{
		allEnemiesKilled = new List<Enemy>();
		roomsComplete = 0;
		goldCollected = 0;
	}

	public void RoomCompleted()
	{
		roomsComplete++;
	}

	public void GoldCollected(int amount)
	{
		goldCollected += amount;

	}

	public int GetEnemiesKilled()
	{
		return allEnemiesKilled.Count;
	}

	public int GetEnemiesKilled(string name)
	{
		int count = 0;

		for (int i = 0; i < allEnemiesKilled.Count; i++)
		{
			if (allEnemiesKilled[i].GetName() == name)
				count++;
		}

		return count;
	}





}
