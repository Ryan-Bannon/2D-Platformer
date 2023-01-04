using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Animator anim; 
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("bullet_Anim");
    }
    void Update()
    {
        if (transform.position.y <= -10)
            Destroy(gameObject);
    }
    void OnCollisionEnter2D(Collision2D target)
    {  
        //Checks to see if the bullet touches the ground, then destroys it if it does
        if(target.gameObject.tag == "ground" || target.gameObject.tag == "player" || target.gameObject.tag == "deadly" || target.gameObject.tag == "boss")
        {
            Destroy(gameObject);
        }
    }    
}
 