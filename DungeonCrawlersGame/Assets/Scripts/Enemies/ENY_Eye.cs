using UnityEngine;
using System.Collections;

public class ENY_Eye : Enemy 
{
    [Header("EnemyAI")]
    public float speed = 1;
    public int numberOfDirections = 4;
    public float wallDistanceCheck = 2;

    float timer = 0;
    public float turnTime = 1;

    Vector2 direction = Vector2.up;

    private Animator anim;
    private int animDirection;
    public GameObject laser;

    void Start()
    {
        direction = RandDirection();

        anim = GetComponent<Animator>();
    }

    public override void UpdateOverride()
    {
        /*rb.AddForce(direction * speed * globalMoveSpeed);

        if (timerFreeze <= 0)
        {
            timer += Time.deltaTime;

            if (timer > turnTime)
            {
                timer = 0;
                int count = 0;
                do
                {
                    count++;
                    if (count > 100)
                        break;
                    direction = RandDirection();
                    Debug.DrawRay(transform.position, direction * wallDistanceCheck, Color.red, 0.5f);
                } while (WallInFrontCheck(direction));
                Debug.DrawRay(transform.position, direction * wallDistanceCheck, Color.green, 0.5f);
            }
        }

        // Update animation state
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                animDirection = (int)Facing.RIGHT;
            else
                animDirection = (int)Facing.LEFT;
        }
        else
        {
            if (direction.y > 0)
                animDirection = (int)Facing.UP;
            else
                animDirection = (int)Facing.DOWN;
        }
        anim.SetInteger("Facing", animDirection);

        // Laser stuff
        laser.transform.rotation = Quaternion.FromToRotation(Vector2.up, -direction);
		 */
		
		if(Vector2.Distance(transform.position, GameManager.inst.player.transform.position) < 10.0f)
			transform.position = Vector2.MoveTowards((Vector2)transform.position, (Vector2)GameManager.inst.player.transform.position, 0.04f);
    }

    Vector2 RandDirection()
    {
        Vector2 temp = Vector2.up;
        float randAmount = Random.Range(0, numberOfDirections) * (360 / numberOfDirections);
        return Quaternion.Euler(0, 0, randAmount) * temp;
    }

    bool WallInFrontCheck(Vector2 dir)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir, wallDistanceCheck);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "Wall")
            {
                return true;
            }
        }
        return false;
    }
}
