using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum UIState
{
    GAME = 0,
    MENU,
    STATS,
    INVENTORY,
    SHOP
}

public class UI_Game : MonoBehaviour 
{
    public EventSystem es;
    public GameObject UIGame;
    public GameObject UIMenu;
    public GameObject UIStats;
    public GameObject UIInventory;
    public GameObject defaultSelection;

    public UIState state = UIState.GAME;
    public Text goldText;
    public Image imgWep1;
    public Image imgWep2;
    public Image imgPass1;
    public Image imgPass2;
    public Image imgPass3;
    public Text healthText;

    void Update()
    {
        if(Input.GetButtonDown("Start"))
        {
            // Start will open the menu if in game, or will close any other menu screen and return to game.
            switch((int)state)
            {
                case 0: // Game
                    state = UIState.MENU;
                    GameManager.inst.player.GetComponent<Player>().controlsEnabled = false;
                    break;
                default:
                    state = UIState.GAME;
                    GameManager.inst.player.GetComponent<Player>().controlsEnabled = true;
                    break;
            }
            UpdateUIState();
        }


        goldText.text = "" + GameManager.inst.inventory.gold;
        healthText.text = GameManager.inst.stats.hpCur + "/" + GameManager.inst.stats.hpMax;

        // Will need to move these into a function call when required, rather than every update.
        imgWep1.sprite = GameManager.inst.activeItems.wepSlot1.GetComponent<SpriteRenderer>().sprite;

        if(GameManager.inst.activeItems.wepSlot2)
            imgWep2.sprite = GameManager.inst.activeItems.wepSlot2.GetComponent<SpriteRenderer>().sprite;
    }

    void UpdateUIState()
    {
        UIGame.SetActive(false);
        UIMenu.SetActive(false);
        UIStats.SetActive(false);
        UIInventory.SetActive(false);

        switch((int)state)
        {
            case 0: // Game
                UIGame.SetActive(true);
                break;
            case 1: // Menu
                UIMenu.SetActive(true);
                es.SetSelectedGameObject(defaultSelection);
                break;
            case 2: // Stats
                UIStats.SetActive(true);
                break;
            case 3: // Inventory
                UIInventory.SetActive(true);
                break;
            default:
                break;
        }
    }
}

/*
switch((int)state)
{
    case 0: // Game
        break;
    case 1: // Menu
        break;
    case 2: // Stats
        break;
    case 3: // Inventory
        break;
    default:
        break;
}
*/