using UnityEngine;
using System.Collections;

public class Weapon : ItemBase 
{
    public int dmg;         // Damage
    public int minDmg;      // Rarity Damage Range (Minimum)
    public int maxDmg;      // Rarity Damage Range (Maximum)
    public int cd;          // Cooldown
    public int ammCur;      // Current Ammo
    public int ammMax;      // Maximum Ammo

    private PolygonCollider2D col;

    void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        type = ItemType.WEAPON;
    }

    void Attack()
    {

    }
}
