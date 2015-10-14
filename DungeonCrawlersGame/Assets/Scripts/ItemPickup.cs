using UnityEngine;
using System.Collections;

public class ItemPickup : MonoBehaviour 
{
    public GameObject item;
    public GameObject sprite;

    void Update()
    {
        sprite.GetComponent<SpriteRenderer>().sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            item = GameManager.inst.activeItems.Pickup(item);

            AudioManager.Inst.PlaySFX(AudioManager.Inst.a_pickupWeapon);
        }
    }
}
