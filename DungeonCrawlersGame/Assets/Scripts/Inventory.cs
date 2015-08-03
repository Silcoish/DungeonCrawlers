using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour 
{
    public static Inventory inst;

    public int gold;
    public List<GameObject> inventory;

    void Awake()
    {
        if(Inventory.inst == null)
        {
            Inventory.inst = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
