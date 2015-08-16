using UnityEngine;
using System.Collections;

public class Weapon : ItemBase 
{
    public int dmg;         // Damage
    public int minDmg;      // Rarity Damage Range (Minimum)
    public int maxDmg;      // Rarity Damage Range (Maximum)
    public int cd;          // Cooldown
    public int kb;          // Knockback Distance
    public int ammCur;      // Current Ammo
    public int ammMax;      // Maximum Ammo
    public GameObject projectile;

    private PolygonCollider2D col;

    void Start()
    {
        col = GetComponent<PolygonCollider2D>();
        type = ItemType.WEAPON;
    }

    public void Attack()
    {
        Vector2 tempDir;

        switch((int)GameManager.inst.player.GetComponent<Player>().dir)
        {
            case 0:
                tempDir = Vector2.down;
                break;
            case 1:
                tempDir = Vector2.up;
                break;
            case 2:
                tempDir = Vector2.right;
                break;
            default:
                tempDir = Vector2.left;
                break;
        }

        //GameObject tempProj = Instantiate((GameObject)projectile, gameObject.transform.position, gameObject.transform.rotation);
    }
}
