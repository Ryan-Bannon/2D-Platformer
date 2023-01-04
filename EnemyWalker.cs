using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : MonoBehaviour
{
    //Will be used to help the enemy change direction when it encounters the edge of the ground
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private Transform startPos, endPos;
    //Will check for ground
    private bool collision;
    //Allows the enemy to walk on the ground
    public float speed = 2f;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    //Update is called once per frame
    void FixedUpdate()
    {
        Move();
        ChangeDirection();
    }

    void Move()
    {
        myRigidbody.velocity = new Vector2(transform.localScale.x, 0) * speed;
    }

    void ChangeDirection()
    {
        collision = Physics2D.Linecast( startPos.position, endPos.position, 1 << LayerMask.NameToLayer("ground") );

        if (!collision)
        {
            Vector3 temp = transform.localScale;
            if (temp.x == 1)
                temp.x = -1f;
            else
                temp.x = 1f;
            transform.localScale = temp;
        }
    }

}
