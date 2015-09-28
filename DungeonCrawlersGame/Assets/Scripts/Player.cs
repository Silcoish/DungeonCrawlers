using UnityEngine;
using System.Collections;

public enum Facing
{
    DOWN = 0,
    UP,
    RIGHT,
    LEFT
}

public class Player : MonoBehaviour 
{
	[System.Serializable]
	public struct RoomData
	{
		public int x;
		public int y;
		public GameObject currentRoom;
	};

    private BoxCollider2D boxCol2D;
    private Rigidbody2D rb2D;
    private Animator anim;
    public Animator armRight;
    public Animator armLeft;
    private SpriteRenderer armRightSprite;
    private SpriteRenderer armLeftSprite;
    public SpriteRenderer wepRight;
    public PolygonCollider2D wepColliderRight;
    
	public RoomData roomData;
	public WeaponCollider weaponCollider;
    public int baseMoveSpeed = 10;
    public bool controlsEnabled = true;
    public float swingColliderUptime = 0.5F;
    private float swingTimerRight = 0;
    private float cdRight;
    private float cdRightCur;
    public Facing dir;

    // The Left Arm
    
    //public SpriteRenderer wepLeft;
    //public PolygonCollider2D wepColliderLeft;
    //private float swingTimerLeft = 0;
    //private float cdLeft;
    //private float cdLeftCur;

    void Start () 
    {
        boxCol2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        armRightSprite = armRight.gameObject.GetComponent<SpriteRenderer>();
        armLeftSprite = armLeft.gameObject.GetComponent<SpriteRenderer>();

		weaponCollider = GetComponent<WeaponCollider>();

		//roomData = new RoomData();
		//roomData.x = 10;

        //For testing
        UpdateEquippedItems();
	}
	
	void Update () 
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

            int direction;

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
                    //wepLeft.sortingOrder = 3;
                    wepRight.sortingOrder = 3;

                    wepColliderRight.transform.eulerAngles = new Vector3(0, 0, 0);
                    //wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, 0);

                    if (!armRight.GetCurrentAnimatorStateInfo(1).IsName("Default"))
                        armRightSprite.sortingOrder = 2;

                    if (!armLeft.GetCurrentAnimatorStateInfo(1).IsName("Default"))
                        armLeftSprite.sortingOrder = 2;
                    break;
                case 1:
                    armRightSprite.sortingOrder = -1; // -1
                    armLeftSprite.sortingOrder = -1;
                    //wepLeft.sortingOrder = -2;
                    wepRight.sortingOrder = -2;

                    wepColliderRight.transform.eulerAngles = new Vector3(0, 0, 180);
                    //wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 2:
                    armLeftSprite.sortingOrder = -2; // -2
                    //wepLeft.sortingOrder = -1;
                    wepRight.sortingOrder = 2;
                    armRightSprite.sortingOrder = 3;

                    wepColliderRight.transform.eulerAngles = new Vector3(0,0,90);
                    //wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 3:
                    armLeftSprite.sortingOrder = 3;
                    //wepLeft.sortingOrder = 2;
                    wepRight.sortingOrder = -1;
                    armRightSprite.sortingOrder = -2; //-2

                    wepColliderRight.transform.eulerAngles = new Vector3(0, 0, -90);
                    //wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, -90);
                    break;
            }

            //if (Mathf.Abs(facing.x) > Mathf.Abs(facing.y))
            //{
            //    if (facing.x > 0)
            //    {
            //        dir = Facing.RIGHT;
            //        anim.SetInteger("Facing", (int)Facing.RIGHT);
            //        armRight.SetInteger("Facing", (int)Facing.RIGHT);
            //        armLeft.SetInteger("Facing", (int)Facing.RIGHT);
            //        armLeftSprite.sortingOrder = -2; // -2
            //        wepLeft.sortingOrder = -1;
            //        wepRight.sortingOrder = 2;
            //        armRightSprite.sortingOrder = 3;

            //        wepColliderRight.transform.eulerAngles = new Vector3(0,0,90);
            //        wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, 90);
            //    }

            //    else
            //    {
            //        dir = Facing.LEFT;
            //        anim.SetInteger("Facing", (int)Facing.LEFT);
            //        armRight.SetInteger("Facing", (int)Facing.LEFT);
            //        armLeft.SetInteger("Facing", (int)Facing.LEFT);
            //        armLeftSprite.sortingOrder = 3;
            //        wepLeft.sortingOrder = 2;
            //        wepRight.sortingOrder = -1;
            //        armRightSprite.sortingOrder = -2; //-2

            //        wepColliderRight.transform.eulerAngles = new Vector3(0, 0, -90);
            //        wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, -90);
            //    }
            //}
            //else
            //{
            //    if (facing.y > 0)
            //    {
            //        dir = Facing.UP;
            //        anim.SetInteger("Facing", (int)Facing.UP);
            //        armRight.SetInteger("Facing", (int)Facing.UP);
            //        armLeft.SetInteger("Facing", (int)Facing.UP);
            //        armRightSprite.sortingOrder = -1; // -1
            //        armLeftSprite.sortingOrder = -1;
            //        wepLeft.sortingOrder = -2;
            //        wepRight.sortingOrder = -2;

            //        wepColliderRight.transform.eulerAngles = new Vector3(0, 0, 180);
            //        wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, 180);
            //    }
            //    else
            //    {
            //        dir = Facing.DOWN;
            //        anim.SetInteger("Facing", (int)Facing.DOWN);
            //        armRight.SetInteger("Facing", (int)Facing.DOWN);
            //        armLeft.SetInteger("Facing", (int)Facing.DOWN);
            //        armRightSprite.sortingOrder = -1;
            //        armLeftSprite.sortingOrder = -1;
            //        wepLeft.sortingOrder = 3;
            //        wepRight.sortingOrder = 3;

            //        wepColliderRight.transform.eulerAngles = new Vector3(0, 0, 0);
            //        wepColliderLeft.transform.eulerAngles = new Vector3(0, 0, 0);

            //        if (!armRight.GetCurrentAnimatorStateInfo(1).IsName("Default"))
            //            armRightSprite.sortingOrder = 2;

            //        if (!armLeft.GetCurrentAnimatorStateInfo(1).IsName("Default"))
            //            armLeftSprite.sortingOrder = 2;
            //    }
            //}

            float xbTriggers = Input.GetAxisRaw("Fire");

            //if ((Input.GetButton("Fire2") || xbTriggers <= -1) && cdRightCur < 0)
            //{
            //    AttackRightHand();
            //}
            //if ((Input.GetButton("Fire1") || xbTriggers >= 1) && cdLeftCur < 0)
            //{
            //    AttackLeftHand();
            //}

            // Attack
            if ((Input.GetButton("Fire1") || xbTriggers <= -1) && cdRightCur < 0)
            {
                AttackRightHand();
            }

            // Weapon Swap
            if(Input.GetButtonDown("Fire2"))
            {
                GameManager.inst.activeItems.SwapRightHand();
                UpdateEquippedItems();
            }
        }
        
        // Update Cooldown timers
        //cdLeftCur -= Time.deltaTime;
        cdRightCur -= Time.deltaTime;

        // Update Swing timers
        if (swingTimerRight < 0)
            wepColliderRight.gameObject.SetActive(false);
        //if (swingTimerLeft < 0)
            //wepColliderLeft.gameObject.SetActive(false);

        swingTimerRight -= Time.deltaTime;
        //swingTimerLeft -= Time.deltaTime;
    }

    public void OnTakeDamage(int dmg, Vector2 kb)
    {
        GameManager.inst.stats.hpCur -= dmg;

        rb2D.AddForce(kb, ForceMode2D.Impulse);

		if (GameManager.inst.stats.hpCur <= 0)
		{
			OnDeath();

		}
    }

	void OnDeath()
	{
		GameManager.inst.stats.hpCur = GameManager.inst.stats.hpMax;
		Application.LoadLevel(1);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
    }

    void OnTriggerStay2D(Collider2D col)
    {
    }

    //void AttackLeftHand()
    //{
    //    weaponCollider.CreateMesh(wepColliderLeft);
    //    armLeft.SetTrigger("Attack");
    //    //GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>().Attack();
    //    wepColliderLeft.gameObject.SetActive(true);
    //    cdLeftCur = cdLeft;
    //    swingTimerLeft = swingColliderUptime;
    //}

    void AttackRightHand()
    {
		weaponCollider.CreateMesh(wepColliderRight);
        armRight.SetTrigger("Attack");
        //GameManager.inst.activeItems.wepRight.GetComponent<Weapon>().Attack();
        wepColliderRight.gameObject.SetActive(true);
        cdRightCur = cdRight;
        swingTimerRight = swingColliderUptime;
    }

    // Update the weapon sprites and collider volumes on the player based on the data in the equipped weapon prefabs.
    void UpdateEquippedItems()
    {
        // Not sure how much of a performance hit this is. Alternatively we could reference these components in the weapon script. (1 GetComponent per weapon)
        wepRight.sprite = GameManager.inst.activeItems.wepRight.GetComponent<SpriteRenderer>().sprite;
        //wepLeft.sprite = GameManager.inst.activeItems.wepLeft.GetComponent<SpriteRenderer>().sprite;
        wepColliderRight.points = GameManager.inst.activeItems.wepRight.GetComponent<PolygonCollider2D>().points;
        //wepColliderLeft.points = GameManager.inst.activeItems.wepLeft.GetComponent<PolygonCollider2D>().points;
        cdRight = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>().cd;
        //cdLeft = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>().cd;
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

	public void SetRoomData(int xx, int yy, GameObject room)
	{
		roomData.x = xx;
		roomData.y = yy;
		roomData.currentRoom = room;
	}
}
