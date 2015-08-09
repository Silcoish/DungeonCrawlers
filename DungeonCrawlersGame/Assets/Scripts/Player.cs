﻿using UnityEngine;
using System.Collections;

enum Facing
{
    DOWN = 0,
    UP,
    RIGHT,
    LEFT
}

public class Player : MonoBehaviour 
{
    private BoxCollider2D boxCol2D;
    private Rigidbody2D rb2D;
    private Animator anim;
    public Animator animLegs;
    public Animator armRight;
    public Animator armLeft;
    private SpriteRenderer armRightSprite;
    private SpriteRenderer armLeftSprite;
    public SpriteRenderer wepRight;
    public SpriteRenderer wepLeft;
    public PolygonCollider2D wepColliderRight;
    public PolygonCollider2D wepColliderLeft;

    public int baseMoveSpeed = 10;
    private float cdRight;
    private float cdRightCur;
    private float cdLeft;
    private float cdLeftCur;

	void Start () 
    {

        boxCol2D = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        armRightSprite = armRight.gameObject.GetComponent<SpriteRenderer>();
        armLeftSprite = armLeft.gameObject.GetComponent<SpriteRenderer>();

        //For testing
        UpdateEquippedItems();
	}
	
	void Update () 
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb2D.AddForce(new Vector2(gameObject.transform.right.x * horizontal, gameObject.transform.up.y * vertical) * (baseMoveSpeed + GameManager.inst.stats.spd));

        Vector3 facing = mousePosition - transform.position;
        anim.SetFloat("MoveSpeed", (Mathf.Abs(horizontal) + Mathf.Abs(vertical))); // Get ANY movement on either axis.

        if(Mathf.Abs(facing.x) > Mathf.Abs(facing.y))
        {
            armRight.gameObject.SetActive(true);
            armLeft.gameObject.SetActive(true);

            if(facing.x > 0)
            {
                anim.SetInteger("Facing", (int)Facing.RIGHT);
                animLegs.SetInteger("Facing", (int)Facing.RIGHT);
                armRight.SetInteger("Facing", (int)Facing.RIGHT);
                armLeft.SetInteger("Facing", (int)Facing.RIGHT);
                armLeftSprite.sortingOrder = -2;
                wepLeft.sortingOrder = -1;
                wepRight.sortingOrder = 2;
                armRightSprite.sortingOrder = 3;
            }
                
            else
            {
                anim.SetInteger("Facing", (int)Facing.LEFT);
                animLegs.SetInteger("Facing", (int)Facing.LEFT);
                armRight.SetInteger("Facing", (int)Facing.LEFT);
                armLeft.SetInteger("Facing", (int)Facing.LEFT);
                armLeftSprite.sortingOrder = 3;
                wepLeft.sortingOrder = 2;
                wepRight.sortingOrder = -1;
                armRightSprite.sortingOrder = -2;
            }
        }
        else
        {
            armRight.gameObject.SetActive(false);
            armLeft.gameObject.SetActive(false);

            if (facing.y > 0)
            {
                anim.SetInteger("Facing", (int)Facing.UP);
                animLegs.SetInteger("Facing", (int)Facing.UP);
                wepLeft.sortingOrder = -1;
                wepRight.sortingOrder = -1;

            }  
            else
            {
                anim.SetInteger("Facing", (int)Facing.DOWN);
                animLegs.SetInteger("Facing", (int)Facing.DOWN);
                wepLeft.sortingOrder = 2;
                wepRight.sortingOrder = 2;
            }
        }

        if(Input.GetButton("Fire1") && cdRightCur < 0)
        {
            AttackRightHand();
        }
        if (Input.GetButton("Fire2") && cdLeftCur < 0)
        {
            AttackLeftHand();
        }

        // Update Cooldown timers
        cdLeftCur -= Time.deltaTime;
        cdRightCur -= Time.deltaTime;
    }

    public void TakeDamage(int dmg, Vector2 pos, int kb)
    {
        GameManager.inst.stats.hpCur -= dmg;

        rb2D.AddForce(((Vector2)transform.position - pos) * kb, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
    }

    void AttackLeftHand()
    {
        anim.SetTrigger("AttackLeft");
        GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>().Attack();
        cdLeftCur = cdLeft;
    }

    void AttackRightHand()
    {
        anim.SetTrigger("AttackRight");
        GameManager.inst.activeItems.wepRight.GetComponent<Weapon>().Attack();
        cdRightCur = cdRight;
    }

    // Update the weapon sprites and collider volumes on the player based on the data in the equipped weapon prefabs.
    void UpdateEquippedItems()
    {
        // Not sure how much of a performance hit this is. Alternatively we could reference these components in the weapon script. (1 GetComponent per weapon)
        wepRight.sprite = GameManager.inst.activeItems.wepRight.GetComponent<SpriteRenderer>().sprite;
        wepLeft.sprite = GameManager.inst.activeItems.wepLeft.GetComponent<SpriteRenderer>().sprite;
        wepColliderRight.points = GameManager.inst.activeItems.wepRight.GetComponent<PolygonCollider2D>().points;
        wepColliderLeft.points = GameManager.inst.activeItems.wepLeft.GetComponent<PolygonCollider2D>().points;
        cdRight = GameManager.inst.activeItems.wepRight.GetComponent<Weapon>().cd;
        cdLeft = GameManager.inst.activeItems.wepLeft.GetComponent<Weapon>().cd;
    }
}
