using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
	[Header("Stats")]
    public int hpCur;   // Health Current
    public int hpMax;   // Health Max
    public int str;     // Strength (Attack Damage)
    public int def;     // Defense
    public int crit;    // Critical Damage
    public int spd;     // Movement Speed
    public int luck;    // Luck

	[Header("Base Costs")]
	public int baseHPCost;
	public int baseStrCost;
	public int baseDefCost;
	public int baseCritCost;
	public int baseSpdCost;
	public int baseLuckCost;

	[Header("Price Increase")]
	public int increasePerUpgradeHPCost;
	public int increasePerUpgradeStrCost;
	public int increasePerUpgradeDefCost;
	public int increasePerUpgradeCritCost;
	public int increasePerUpgradeSpdCost;
	public int increasePerUpgradeLuckCost;

	[Header("Stat Increase")]
	public int statIncreasePerUpgradeHPCost;
	public int statIncreasePerUpgradeStrCost;
	public int statIncreasePerUpgradeDefCost;
	public int statIncreasePerUpgradeCritCost;
	public int statIncreasePerUpgradeSpdCost;
	public int statIncreasePerUpgradeLuckCost;

	[HideInInspector] public int hpUpgrades = 0;
	[HideInInspector] public int strUpgrades = 0;
	[HideInInspector] public int defUpgrades = 0;
	[HideInInspector] public int critUpgrades = 0;
	[HideInInspector] public int spdUpgrades = 0;
	[HideInInspector] public int luckUpgrades = 0;

    void Update()
    {
        hpCur = GameManager.inst.player.GetComponent<Damageable>().hitPoints;
    }

	public void IncreaseHP()
	{
		hpMax += statIncreasePerUpgradeHPCost;
		hpCur += statIncreasePerUpgradeHPCost;
		hpUpgrades++;
	}

	public void IncreaseStr()
	{
		str += statIncreasePerUpgradeStrCost;
		strUpgrades++;
	}

	public void IncreaseDef()
	{
		def += statIncreasePerUpgradeDefCost;
		defUpgrades++;
	}

	public void IncreaseCrit()
	{
		crit += statIncreasePerUpgradeCritCost;
		critUpgrades++;
	}

	public void IncreaseSpd()
	{
		spd += statIncreasePerUpgradeSpdCost;
		spdUpgrades++;
	}

	public void IncreaseLuck()
	{
		luck += statIncreasePerUpgradeLuckCost;
		luckUpgrades++;
	}
}
