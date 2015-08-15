using UnityEngine;
using System.Collections;

public class QST_KillEnemies : Quest 
{
	public string enemyName;
	public int kills;


	override public bool CheckProgress()
	{
		if (GameManager.inst.gameDataManager.GetEnemiesKilled(enemyName) >= kills)
			return true;
		else return false;
	}

	public override void Randomise()
	{
		//Randomly sets the Enemy and kills
	}
}
