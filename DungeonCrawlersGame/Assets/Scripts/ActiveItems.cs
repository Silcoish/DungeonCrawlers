using UnityEngine;
using System.Collections;

public class ActiveItems : MonoBehaviour 
{
    //public GameObject wepLeft;         // Left Equipped Weapon
    public GameObject wepRight;        // Right Equipped Weapon
    //public GameObject wepLeftOff;      // Left Secondary Weapon
    public GameObject wepRightOff;     // Right Secondary Weapon
    public GameObject pas1;            // Passive Item 1
    public GameObject pas2;            // Passive Item 2
    public GameObject pas3;            // Passive Item 3

    private float WRcd;
    private float WRcdCur;
    private float WROcd;
    private float WROcdCur;

    void Update()
    {
        WRcd = wepRight.GetComponent<Weapon>().cd;
        WROcd = wepRightOff.GetComponent<Weapon>().cd;

        // Update Cooldown Timers
        WRcdCur -= Time.deltaTime;
        WROcdCur -= Time.deltaTime;
    }

    public bool IsReady()
    {
        if (WRcdCur < 0)
            return true;
        else
            return false;
    }

    public void ResetTimer()
    {
        WRcdCur = WRcd;
    }

    //void SwapLeftHand()
    //{
    //    GameObject temp = wepLeft;
    //    wepLeft = wepLeftOff;
    //    wepLeftOff = temp;
    //}

    public void SwapRightHand()
    {
        GameObject temp = wepRight;
        wepRight = wepRightOff;
        wepRightOff = temp;

        // Swap cooldown timers
        float cdTemp = WRcd;
        float cdCurTemp = WRcdCur;
        WRcd = WROcd;
        WRcdCur = WROcdCur;
        WROcd = cdTemp;
        WROcdCur = cdCurTemp;
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
