using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
    public int hpCur;   // Health Current
    public int hpMax;   // Health Max
    public int str;     // Strength (Attack Damage)
    public int def;     // Defense
    public int crit;    // Critical Damage
    public int spd;     // Movement Speed
    public int luck;    // Luck

    void Update()
    {
        hpCur = GameManager.inst.player.GetComponent<Damageable>().hitPoints;
    }
}
