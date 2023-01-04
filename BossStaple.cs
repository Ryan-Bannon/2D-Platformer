using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStaple : MonoBehaviour
{
    //Accesses the boss
    private GameObject boss;
    //Accesses the staple's rigid body
    private Rigidbody2D myRigidbody;
    //The speed at which the staple moves
    public float stapleSpeed;
    //The boss's transform scale
    private Vector3 bossScale;
   
    
    void Awake()
    {
        Vector3 temp = transform.localScale;
        boss = GameObject.Find("Stapler Idle");
        myRigidbody = GetComponent<Rigidbody2D>();
        stapleSpeed = boss.GetComponent<EnemyBoss>().stapleSpeed;
        bossScale = boss.transform.localScale;

        //Checks if the boss is facing left, and if it is, then it puts the staple velocity in the left direction and puts the staple facing leftward    
        if (boss.transform.localScale.x < 0)
        {
            temp.x = 1;
            stapleSpeed *= -1;
        }

        //Checks if the boss is facing right, and if it is, then it puts the staple facing leftward   
        else
        {
            temp.x = -1;
        }
        //Sets the staple in the direction specified from the above conditionals   
        transform.localScale = temp;
        
        //Sets the staple's velocity
        myRigidbody.velocity = new Vector2(stapleSpeed, 0);

        //Sets the stapleSpeed back to the original direction 
        stapleSpeed *= -1;
    }
    // Update is called once per frame
    void Update()
    {
        //Checks if the bullet is out of bounds, and if it is, then it destroys the bullet
        if (transform.position.x <= -18 || transform.position.x >= 23)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        //Checks if the staple hits the player, and if it does, then the staple is destroyed
        if (target.gameObject.tag == "player")
        {
            Destroy(gameObject);
        }
    }

}
