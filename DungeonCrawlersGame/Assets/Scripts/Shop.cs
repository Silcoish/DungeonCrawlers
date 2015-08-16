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

    private bool firstOpen = true;
    private GameObject[] inventory = new GameObject[6];
    
    void Update()
    {
        if(firstOpen)
        {
            UpdateInventory();
            firstOpen = false;
        }


    }

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
}
