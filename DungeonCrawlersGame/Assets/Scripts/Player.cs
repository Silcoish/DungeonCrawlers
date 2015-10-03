using UnityEngine;
using System.Collections;

public enum Facing
{
    DOWN = 0,
    UP,
    RIGHT,
    LEFT
}

public class Player : Damageable 
{
    private BoxCollider2D boxCol2D;
    private Rigidbody2D rb2D;
    private Animator anim;
    public Animator armRight;
    public Animator armLeft;
    private SpriteRenderer armRightSprite;
    private SpriteRenderer armLeftSprite;
    public SpriteRenderer wepSprite;
    public PolygonCollider2D wepCollider;
    
    public Transform currentRoom;
	
    public WeaponCollider weaponCollider;

    public bool controlsEnabled = true;
    public float swingColliderUptime = 0.5F;
    private float swingTimerRight = 0;

    public int baseMoveSpeed = 10;
    public float cdSwap = 0.5F;
    private float cdSwapCur;
    public int direction;

    void Start() 
    {
        boxCol2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        armRightSprite = armRight.gameObject.GetComponent<SpriteRenderer>();
        armLeftSprite = armLeft.gameObject.GetComponent<SpriteRenderer>();

		weaponCollider = GetComponent<WeaponCollider>();

        UpdateEquippedItems();
        hitPoints = GameManager.inst.stats.hpCur;
	}
	
	public override void UpdateOverride() 
    {
        if(controlsEnabled)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            rb2D.AddForce(new Vector2(gameObject.transform.right.x * horizontal, gameObject.transform.up.y * vertical) * (baseMoveSpeed + GameManager.inst.stats.spd));

            anim.SetFloat("MoveSpeed", (Mathf.Abs(horizontal) + Mathf.Abs(vertical))); // Get ANY movement on either axis.
            armRight.SetFloat("MoveSpeed", (Mathf.Abs(horizontal) + Mathf.Abs(vertical))); // Get ANY movement on either axis.
            armLeft.SetFloat("MoveSpeed", (Mathf.Abs(horizontal) + Mathf.Abs(vertical))); // NOTE: This needs to stay despite the removal of Left Weapons (this controls ALL left arm animations).

            if(GameManager.inst.useMouseControls)
            {
                Vector3 facing = mousePosition - transform.position;

                if (Mathf.Abs(facing.x) > Mathf.Abs(facing.y))
                {
                    if (facing.x > 0)
                        direction = (int)Facing.RIGHT;
                    else
                        direction = (int)Facing.LEFT;
                }
                else
                {
                    if (facing.y > 0)
                        direction = (int)Facing.UP;
                    else
                        direction = (int)Facing.DOWN;
                }
            }
            else
            {
                direction = GetDirection();
            }

            anim.SetInteger("Facing", direction);
            armRight.SetInteger("Facing", direction);
            armLeft.SetInteger("Facing", direction);

            switch(direction)
            {
                case 0:
                    armRightSprite.sortingOrder = -1;
                    armLeftSprite.sortingOrder = -1;
                    wepSprite.sortingOrder = 3;

                    wepCollider.transform.eulerAngles = new Vector3(0, 0, 0);

                    if (!armRight.GetCurrentAnimatorStateInfo(1).IsName("Default"))
                        armRightSprite.sortingOrder = 2;

                    if (!armLeft.GetCurrentAnimatorStateInfo(1).IsName("Default"))
                        armLeftSprite.sortingOrder = 2;
                    break;
                case 1:
                    armRightSprite.sortingOrder = -1;
                    armLeftSprite.sortingOrder = -1;
                    wepSprite.sortingOrder = -2;

                    wepCollider.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 2:
                    armLeftSprite.sortingOrder = -2;
                    wepSprite.sortingOrder = 2;
                    armRightSprite.sortingOrder = 3;

                    wepCollider.transform.eulerAngles = new Vector3(0,0,90);
                    break;
                case 3:
                    armLeftSprite.sortingOrder = 3;
                    wepSprite.sortingOrder = -1;
                    armRightSprite.sortingOrder = -2;

                    wepCollider.transform.eulerAngles = new Vector3(0, 0, -90);
                    break;
            }

            float xbTriggers = Input.GetAxisRaw("Fire");

            // Attack
            if ((Input.GetButton("Fire1") || xbTriggers <= -1) && cdSwapCur < 0 && GameManager.inst.activeItems.IsReady())
            {
                Attack();
            }

            // Weapon Swap
            if(Input.GetButtonDown("Fire2"))
            {
                if (GameManager.inst.activeItems.SwapWeapon())
                    cdSwapCur = cdSwap;

                UpdateEquippedItems();
            }

            // Kill Switch ---REMOVE AFTER TESTING---
            if(Input.GetKeyDown(KeyCode.K))
            {
                OnDeath();
            }
        }
        
        // Disable wepCollider at end of swing
        if (swingTimerRight < 0)
            wepCollider.gameObject.SetActive(false);

        // Update timers
        cdSwapCur -= Time.deltaTime;
        swingTimerRight -= Time.deltaTime;
    }

	public override void OnDeath()
	{
        GameManager.inst.stats.hpCur = hitPoints = GameManager.inst.stats.hpMax; // Reset Health

        GameManager.inst.inventory.gold = 0; // How do we handle this when passives can affect gold loss?

        // Destroy non permament items
        GameManager.inst.activeItems.wepSlot2 = null;
        GameManager.inst.activeItems.pasSlot2 = null;
        GameManager.inst.activeItems.pasSlot3 = null;

		Application.LoadLevel(1);
	}

    void Attack()
    {
		weaponCollider.CreateMesh(wepCollider);
        armRight.SetTrigger("Attack");
        wepCollider.gameObject.SetActive(true);
        GameManager.inst.activeItems.ResetTimer();
        swingTimerRight = swingColliderUptime;

        GameManager.inst.activeItems.wepSlot1.GetComponent<Weapon>().Attack();
    }

    // Update the weapon sprites and collider volumes on the player based on the data in the equipped weapon prefabs.
    public void UpdateEquippedItems()
    {
        // Not sure how much of a performance hit this is. Alternatively we could reference these components in the weapon script. (1 GetComponent per weapon)
        
        wepSprite.sprite = GameManager.inst.activeItems.wepSlot1.GetComponent<SpriteRenderer>().sprite;
        wepCollider.points = GameManager.inst.activeItems.wepSlot1.GetComponent<PolygonCollider2D>().points;
    }

    void SetSpriteArmLayers(int layer)
    {
        armRightSprite.sortingOrder = layer;
        armLeftSprite.sortingOrder = layer;
    }

    int GetDirection()
    {
        float horizontal = Input.GetAxis("Horizontal2");
        float vertical = Input.GetAxis("Vertical2");

        if(Mathf.Abs(vertical) >= Mathf.Abs(horizontal))
        {
            if (vertical >= 0)
                return 0;
            else
                return 1;
        }
        else
        {
            if (horizontal > 0)
                return 2;
            else
                return 3;
        }
    }
}
