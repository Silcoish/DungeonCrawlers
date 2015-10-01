using UnityEngine;
using System.Collections;

public enum StatusEffect { NONE, POISON, BURN, FREEZE, STUN };

public class ItemBase : MonoBehaviour 
{
    public enum ItemType { WEAPON, PASSIVE };
    

    public string name;         // Item name for lookup
    public ItemType type;
    public StatusEffect effect;
    public int price;           // Vendor price
    Sprite sprite;
    BoxCollider2D pickupCol;    // Collider for picking up when dropped as loot
}
