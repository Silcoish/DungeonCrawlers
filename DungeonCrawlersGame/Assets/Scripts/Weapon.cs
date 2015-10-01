using UnityEngine;
using System.Collections;

public class Weapon : ItemBase 
{
    public int dmg;         // Damage
    public float cd;          // Cooldown
    public int kb;          // Knockback Distance
    public int ammCur;      // Current Ammo
    public int ammMax;      // Maximum Ammo
    public bool isRanged = false;
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
        Vector3 tempRot;

        switch((int)GameManager.inst.player.GetComponent<Player>().direction)
        {
            case 0:
                tempDir = Vector2.down;
                tempRot = new Vector3(0, 0, 180);
                break;
            case 1:
                tempDir = Vector2.up;
                tempRot = new Vector3(0, 0, 0);
                break;
            case 2:
                tempDir = Vector2.right;
                tempRot = new Vector3(0, 0, -90);
                break;
            default:
                tempDir = Vector2.left;
                tempRot = new Vector3(0, 0, 90);
                break;
        }

        if(isRanged)
        {
            GameObject player = GameManager.inst.player;
            GameObject tempProj;
            tempProj = Instantiate(projectile, player.transform.position, transform.rotation) as GameObject;
            tempProj.GetComponent<Projectile>().SetDirection(tempDir, tempRot);
        }    
    }

    public Damage GetDamage()
    {
        Damage temp;

        temp.type = effect;
        temp.amount = dmg;
        temp.knockback = kb;
        temp.fromGO = gameObject.transform;

        return temp;
    }
}
