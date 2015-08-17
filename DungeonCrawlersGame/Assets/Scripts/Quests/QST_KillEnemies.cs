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
		else 
			return false;
	}

	public override void Randomise()
	{
		//Randomly sets the Enemy and kills
	}

	public override string GetText()
	{
		int rand = Random.Range(0, 2);

		switch (rand)
		{
			case 0:
				return "My Brother was killed by " + enemyName + "'s.\nCould you kill " + kills + " of them for me.";
			case 1:
				return "I am making a stew and need some " + enemyName + "'s.\nCould you kill " + kills + " of them for me.";
			default:
				return "My Brother was killed by " + enemyName + "'s.\nCould you kill " + kills + " of them for me.";
		}
	}
}
