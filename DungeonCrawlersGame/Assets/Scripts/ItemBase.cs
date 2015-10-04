using UnityEngine;
using System.Collections;

public class ItemBase : MonoBehaviour 
{
    public enum ItemType { WEAPON, PASSIVE };
    
    public string itemName;         // Item name for lookup
    public ItemType type;
    public DamageType effect;
    public float effectDuration;
    public float effectStrength;
    public int price;           // Vendor price
    Sprite sprite;
    BoxCollider2D pickupCol;    // Collider for picking up when dropped as loot

    // THIS GOES ON THE PLAYER WHEN COREY STOPS TOUCHING IT
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            // Instantiate current equipped and drop
            Instantiate(GameManager.inst.activeItems.wepSlot1, transform.position, transform.rotation);

        }
    }
}
