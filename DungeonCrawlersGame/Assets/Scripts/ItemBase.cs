using UnityEngine;
using System.Collections;

public class ItemBase : MonoBehaviour 
{
    public enum ItemType { WEAPON, PASSIVE };
    public enum ItemRarity { UNCOMMON, COMMON, RARE, EPIC, LEGENDARY };

    public string name;         // Item name for lookup
    public ItemType type;
    public ItemRarity rarity;
    public int price;           // Vendor price
    Sprite sprite;
    BoxCollider2D pickupCol;    // Collider for picking up when dropped as loot
}
