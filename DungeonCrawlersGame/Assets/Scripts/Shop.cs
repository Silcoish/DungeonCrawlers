using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shop : MonoBehaviour 
{
    public Image item1;
    public Image item2;
    public Image item3;
    public Image item4;
    public Image item5;
    public Image item6;

	[Header("Stat Title Texts")]
	public Text item1Title;
	public Text item2Title;
	public Text item3Title;
	public Text item4Title;
	public Text item5Title;
	public Text item6Title;

	[Header("Stat Increase Text")]
	public Text item1StatIncreaseText;
	public Text item2StatIncreaseText;
	public Text item3StatIncreaseText;
	public Text item4StatIncreaseText;
	public Text item5StatIncreaseText;
	public Text item6StatIncreaseText;

	[Header("Price Display Texts")]
	public Text item1PriceText;
	public Text item2PriceText;
	public Text item3PriceText;
	public Text item4PriceText;
	public Text item5PriceText;
	public Text item6PriceText;

    private bool firstOpen = true;
    private GameObject[] inventory = new GameObject[6];

	public Stats stats;

	public bool checkForGold = true;
    
    void Update()
    {
        //if(firstOpen)
        //{
        //    UpdateInventory();
        //    firstOpen = false;
        //}
		SetPrices();
    }

	void SetPrices()
	{
		item1Title.text = "Max HP: " + stats.hpMax.ToString();
		item2Title.text = "Strength: " + stats.str.ToString();
		item3Title.text = "Defense: " + stats.def.ToString();
		item4Title.text = "Crit: " + stats.crit.ToString();
		item5Title.text = "Speed: " + stats.spd.ToString();
		item6Title.text = "Luck: " + stats.luck.ToString();

		item1PriceText.text =	HPCost()	.ToString()	+ " Gold";
		item2PriceText.text =	StrCost()	.ToString()	+ " Gold";
		item3PriceText.text =	DefCost()	.ToString()	+ " Gold";
		item4PriceText.text =	CritCost()	.ToString() + " Gold";
		item5PriceText.text =	SpdCost()	.ToString()	+ " Gold";
		item6PriceText.text =	LuckCost()	.ToString() + " Gold";

		item1StatIncreaseText.text = "+" + stats.statIncreasePerUpgradeHPCost	.ToString()	+ " HP";
		item2StatIncreaseText.text = "+" + stats.statIncreasePerUpgradeStrCost	.ToString()	+ " Strength";
		item3StatIncreaseText.text = "+" + stats.statIncreasePerUpgradeDefCost	.ToString()	+ " Defense";
		item4StatIncreaseText.text = "+" + stats.statIncreasePerUpgradeCritCost	.ToString()	+ " Crit";
		item5StatIncreaseText.text = "+" + stats.statIncreasePerUpgradeSpdCost	.ToString()	+ " Speed";
		item6StatIncreaseText.text = "+" + stats.statIncreasePerUpgradeLuckCost	.ToString()	+ " Luck";
	}

	int HPCost()
	{
		return CalculatePrice(stats.baseHPCost, stats.increasePerUpgradeHPCost, stats.hpUpgrades);
	}

	int StrCost()
	{
		return CalculatePrice(stats.baseStrCost, stats.increasePerUpgradeStrCost, stats.strUpgrades);
	}

	int DefCost()
	{
		return CalculatePrice(stats.baseDefCost, stats.increasePerUpgradeDefCost, stats.defUpgrades);
	}

	int CritCost()
	{
		return CalculatePrice(stats.baseCritCost, stats.increasePerUpgradeCritCost, stats.critUpgrades);
	}

	int SpdCost()
	{
		return CalculatePrice(stats.baseSpdCost, stats.increasePerUpgradeSpdCost, stats.spdUpgrades);
	}

	int LuckCost()
	{
		return CalculatePrice(stats.baseLuckCost, stats.increasePerUpgradeLuckCost, stats.luckUpgrades);
	}

	int CalculatePrice(int baseCost, int increasePrice, int amountBought)
	{
		return baseCost + (increasePrice * amountBought);
	}

	public void PurchaseStat(int index)
	{
		switch(index)
		{
			case 0:
				if(checkForGold)
				{
					if(GameManager.inst.inventory.gold >= HPCost())
					{
						GameManager.inst.inventory.gold -= HPCost();
						stats.IncreaseHP();
					}
				}
				else
					stats.IncreaseHP();
				break;
			case 1:
				if (checkForGold)
				{
					if (GameManager.inst.inventory.gold >= StrCost())
					{
						GameManager.inst.inventory.gold -= StrCost();
						stats.IncreaseStr();
					}
				}
				else
					stats.IncreaseStr();
				break;
			case 2:
				if (checkForGold)
				{
					if (GameManager.inst.inventory.gold >= DefCost())
					{
						GameManager.inst.inventory.gold -= DefCost();
						stats.IncreaseDef();
					}
				}
				else
					stats.IncreaseDef();
				break;
			case 3:
				if (checkForGold)
				{
					if (GameManager.inst.inventory.gold >= CritCost())
					{
						GameManager.inst.inventory.gold -= CritCost();
						stats.IncreaseCrit();
					}
				}
				else
					stats.IncreaseCrit();
				break;
			case 4:
				if (checkForGold)
				{
					if (GameManager.inst.inventory.gold >= SpdCost())
					{
						GameManager.inst.inventory.gold -= SpdCost();
						stats.IncreaseSpd();
					}
				}
				else
					stats.IncreaseSpd();
				break;
			case 5:
				if (checkForGold)
				{
					if (GameManager.inst.inventory.gold >= LuckCost())
					{
						GameManager.inst.inventory.gold -= LuckCost();
						stats.IncreaseLuck();
					}
				}
				else
					stats.IncreaseLuck();
				break;
		}

		SetPrices();
	}


	/*
    // Update the contents of the inventory.
    void UpdateInventory()
    {
        Debug.Log("Updating inventory");

        //inventory[0] = GetStock();
        //inventory[1] = GetStock();
        //inventory[2] = GetStock();
        inventory[3] = GameManager.inst.vendorInventory.weapons[0];
        inventory[4] = GameManager.inst.vendorInventory.weapons[1];
        inventory[5] = GameManager.inst.vendorInventory.weapons[2];

        // Update the button sprites
        //item1.sprite = inventory[0].gameObject.GetComponent<SpriteRenderer>().sprite;
        //item2.sprite = inventory[1].gameObject.GetComponent<SpriteRenderer>().sprite;
        //item3.sprite = inventory[2].gameObject.GetComponent<SpriteRenderer>().sprite;
        item4.sprite = inventory[3].gameObject.GetComponent<SpriteRenderer>().sprite;
        item5.sprite = inventory[4].gameObject.GetComponent<SpriteRenderer>().sprite;
        item6.sprite = inventory[5].gameObject.GetComponent<SpriteRenderer>().sprite;

        // Update the GameManager with the newly generated items for next time.
        //GameManager.inst.vendorInventory.weapons[0] = inventory[0];
        //GameManager.inst.vendorInventory.weapons[1] = inventory[1];
        //GameManager.inst.vendorInventory.weapons[2] = inventory[2];
    }

    // Gets new stock from the list?
    GameObject GetStock()
    {
        return null;
    }

    // will need to update with price checks once gold pickup is added
    public void Purchase(int slot)
    {
        GameManager.inst.inventory.inventory.Add(inventory[slot]);
    }
	*/
	
}
