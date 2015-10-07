/* Copyright (c) Dungeon Crawlers
*  Script Created by:
*  Corey Underdown
*/
 
using UnityEngine;
 
public class RoomObject : MonoBehaviour
{
	public GameObject enemiesParent;
	public int enemiesCount;
	public Door doorNorth;
	public Door doorSouth;
	public Door doorEast;
	public Door doorWest;

	public void EnteredRoom()
	{
		enemiesParent.SetActive(true);
		if(enemiesCount > 0)
		{
			LockDoors();
		}
	}

	void LockDoors()
	{
		if (doorNorth != null)
			doorNorth.Lock();
		if (doorSouth != null)
			doorSouth.Lock();
		if (doorEast != null)
			doorEast.Lock();
		if (doorWest != null)
			doorWest.Lock();
	}

	void UnlockDoors()
	{
		if (doorNorth != null)
			doorNorth.Unlock();
		if (doorSouth != null)
			doorSouth.Unlock();
		if (doorEast != null)
			doorEast.Unlock();
		if (doorWest != null)
			doorWest.Unlock();

	}

	public void SetupEnemies()
	{
		for(int i = 0; i < enemiesCount; i++)
		{
			enemiesParent.transform.GetChild(i).GetComponent<Enemy>().room = this;
		}
	}

	public void EnemyDied()
	{
		enemiesCount--;
		if (enemiesCount == 0)
			UnlockDoors();
	}


}