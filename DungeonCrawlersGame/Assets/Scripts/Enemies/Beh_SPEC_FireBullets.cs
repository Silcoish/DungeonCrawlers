using UnityEngine;
using System.Collections;

[System.Serializable]
public class Beh_SPEC_FireBullets : Behaviour 
{
    //enum ShotDir { Forward, Back, Left, Right };

    //public GameObject bullet;
    //[Range(1,36)]
    //public int numberOfBullets = 1;
    //[Range(0,180)]
    //public float shotAngle = 0;//For each side, 180 == 360 shots.
    //[Range(0.1f,50f)]
    //public float shotSpeed;


    //public NoteSubscribe sub;
    //NoteSubscribe.State prevState;
    ///// <summary>
    ///// Only activate every "stateSkips" instance of the note. 
    ///// ie. == 4. Only do every 4 beats.
    ///// </summary>
    //[Range(1, 4)]
    //public int stateSkips = 1;
    //int skipCount = 0;



    //public override void BehaviourUpdate(Enemy en)
    //{
    //    //print("fire");
    //    if (sub.state == NoteSubscribe.State.ACTIVE && prevState == NoteSubscribe.State.DEACTIVE)
    //    {
    //        skipCount++;
    //    }
    //    if (skipCount >= stateSkips)
    //    {
    //        //print("fire");
    //        skipCount = 0;
    //        FireBullets(en);
    //    }
    //    prevState = sub.state;
    //}

    //public void FireBullets(Enemy en)
    //{
    //    //print("fire 2");
    //    Vector2 tempDir = en.GetLookDirection().normalized;
    //    GLobalFunctions.RotateVector(ref tempDir, -shotAngle);
    //    float angleDif = (shotAngle * 2) / numberOfBullets;

    //    for (int i = 0; i < numberOfBullets; i++)
    //    {
    //        FireBullet(tempDir);
    //        GLobalFunctions.RotateVector(ref tempDir, angleDif);
    //    }
    //}

    //public void FireBullet(Vector2 dir)
    //{
    //    //print("fire 3");
    //    GameObject tempBullet = Instantiate(bullet,gameObject.transform.position, gameObject.transform.rotation) as GameObject;
    //    Physics2D.IgnoreCollision(tempBullet.GetComponent<Collider2D>(),gameObject.GetComponent<Collider2D>(), true);
    //    Rigidbody2D bRB = tempBullet.GetComponent<Rigidbody2D>();
    //    bRB.velocity = dir * shotSpeed;
    //}

    //public float RotateDegrees(float deg, float amount)
    //{
    //    float temp = deg + amount;

    //    while (temp < 0)
    //    {
    //        temp += 360;
    //    }
    //    while (temp > 360)
    //    {
    //        temp -= 360;
    //    }

    //    return temp;
    //}


}
