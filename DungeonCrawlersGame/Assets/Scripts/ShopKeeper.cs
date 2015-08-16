using UnityEngine;
using System.Collections;

public class ShopKeeper : MonoBehaviour 
{
    public GameObject shopUI;
    public GameObject defaultSelection;
    public UI_Game uiController; 
    public GameObject button;

    private bool isOpen = false;
    private bool isActive = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            button.SetActive(true);
            isActive = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            button.SetActive(false);
            isActive = false;
        }
    }

    void Update()
    {
        if(isActive)
        {
            if (Input.GetButtonDown("Submit") && !isOpen)
            {
                // Disable Player Controls while shop is open
                GameManager.inst.player.GetComponent<Player>().controlsEnabled = false;

                // Enable the shop UI
                shopUI.SetActive(true);
                isOpen = true;
                uiController.es.SetSelectedGameObject(defaultSelection);
            }

            if ((Input.GetButtonDown("Cancel") || Input.GetButtonDown("Start")) && isOpen)
            {
                // Reenable controls once player exits shop
                GameManager.inst.player.GetComponent<Player>().controlsEnabled = true;

                // Disable shop UI
                shopUI.SetActive(false);
                isOpen = false;
            }
        }
    }
}
