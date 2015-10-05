using UnityEngine;
using System.Collections;

public class ActiveItems : MonoBehaviour 
{
    public GameObject wepSlot1;         // Right Equipped Weapon
    public GameObject wepSlot2;         // Right Secondary Weapon
    public GameObject pasSlot1;         // Passive Item 1
    public GameObject pasSlot2;         // Passive Item 2
    public GameObject pasSlot3;         // Passive Item 3
    public GameObject wepDefault;       // Default Weapon if none are equipped
    private float WS1cd;
    private float WS1cdCur;
    private float WS2cd;
    private float WS2cdCur;

    void Awake()
    {
        // Check if there is a Weapon assigned to Slot1
        if(!wepSlot1)
        {
            // If not check if there is a weapon in slot 2
            if(wepSlot2)
            {
                wepSlot1 = wepSlot2; // And swap it to slot1
                wepSlot2 = null;
            }
            else
                wepSlot1 = wepDefault; // or generate a default weapon in slot1
        }
    }
    
    void Update()
    {
        WS1cd = wepSlot1.GetComponent<Weapon>().cd;
        WS1cdCur -= Time.deltaTime;
        
        if(wepSlot2)
        {
            WS2cd = wepSlot2.GetComponent<Weapon>().cd;
            WS2cdCur -= Time.deltaTime;
        }
    }

    public bool IsReady()
    {
        if (WS1cdCur < 0)
            return true;
        else
            return false;
    }

    public void ResetTimer()
    {
        WS1cdCur = WS1cd;
    }

    public bool SwapWeapon()
    {
        // Check if a weapon is available to swap
        if (!wepSlot2)
            return false;
        else
        {
            // Swap Weapons
            GameObject temp = wepSlot1;
            wepSlot1 = wepSlot2;
            wepSlot2 = temp;

            // Swap cooldown timers
            float cdTemp = WS1cd;
            float cdCurTemp = WS1cdCur;
            WS1cd = WS2cd;
            WS1cdCur = WS2cdCur;
            WS2cd = cdTemp;
            WS2cdCur = cdCurTemp;
        }
        return true;
    }

    void UsePassive(int i)
    {
        switch (i)
        {
            case 1:
                // Call pas1 use function
                break;
            case 2:
                // Call pas2 use function
                break;
            case 3:
                // Call pas3 use function
                break;
            default:
                // Error message?
                break;
        }
    }
}
