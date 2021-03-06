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
    PolygonCollider2D damageCol;

    void Start()
    {
        damageCol = GetComponent<PolygonCollider2D>();
    }

    public Damage GetDamage()
    {
        Damage temp;

        temp.type = effect;
        temp.amount = dmg;
        temp.knockback = kb;
        temp.fromGO = GameManager.inst.player.transform;
        temp.effectTime = effectDuration;
        temp.effectStrength = effectStrength;

        return temp;
    }
}
