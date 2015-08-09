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
        goldText.text = "" + GameManager.inst.inventory.gold;
        healthText.text = GameManager.inst.stats.hpCur + "/" + GameManager.inst.stats.hpMax;

        // Will need to move these into a function call when required, rather than every update.
        wepLeft.sprite = GameManager.inst.activeItems.wepLeft.GetComponent<SpriteRenderer>().sprite;
        wepRight.sprite = GameManager.inst.activeItems.wepRight.GetComponent<SpriteRenderer>().sprite;
    }
	
}
