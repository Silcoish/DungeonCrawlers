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

    //void SwapLeftHand()
    //{
    //    GameObject temp = wepLeft;
    //    wepLeft = wepLeftOff;
    //    wepLeftOff = temp;
    //}

    void SwapRightHand()
    {
        GameObject temp = wepRight;
        wepRight = wepRightOff;
        wepRightOff = temp;
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
