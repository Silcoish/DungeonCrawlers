using UnityEngine;
using System.Collections;

public enum ItemType { WEAPON, PASSIVE };

public class ItemBase : MonoBehaviour 
{
    public string itemName;         // Item name for lookup
    public ItemType type;

    public int dmg;                 // Damage
    public float cd;                // Cooldown
    public int kb;                  // Knockback Distance
    public DamageType effect;
    public float effectDuration;
    public float effectStrength;
    public int price;               // Vendor price
    Sprite sprite;
    BoxCollider2D pickupCol;        // Collider for picking up when dropped as loot

    public Damage GetDamage()
    {
        Damage temp;

        temp.type = effect;
        temp.amount = dmg;
        temp.knockback = kb;
        temp.fromGO = gameObject.transform;
        temp.effectTime = effectDuration;
        temp.effectStrength = effectStrength;

        return temp;
    }

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
