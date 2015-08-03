using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour 
{
    public static Stats inst;
    
    public int hpCur;   // Health Current
    public int hpMax;   // Health Max
    public int str;     // Strength (Attack Damage)
    public int def;     // Defense
    public int crit;    // Critical Damage
    public int luck;    // Luck

    void Awake()
    {
        if(Stats.inst == null)
        {
            Stats.inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
