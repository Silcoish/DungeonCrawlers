using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Game : MonoBehaviour 
{
    public Text goldText;
    public Image wepLeft;
    public Image wepRight;
    public Image wepLeftOff;
    public Image wepRightOff;
    public Image pass1;
    public Image pass2;
    public Image pass3;
    public Text healthText;

    void Update()
    {
        goldText.text = "" + Inventory.inst.gold;
        healthText.text = Stats.inst.hpCur + "/" + Stats.inst.hpMax;

        // Will need to move these into a function call when required, rather than every update.
        wepLeft.sprite = ActiveItems.inst.wepLeft.GetComponent<SpriteRenderer>().sprite;
        wepRight.sprite = ActiveItems.inst.wepRight.GetComponent<SpriteRenderer>().sprite;
    }
	
}
