using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float bulletXDistance;

    [SerializeField]
    private float bulletYDistance;

    private Animator myAnimator;

   // public Vector2 position;

    // Start is called before the first frame update
    void OnEnable()
    {
        myAnimator = GetComponent<Animator>();
        //Calls our attack function to start shooting bullets right from the start of the game
        StartCoroutine(Attack());
    }

    

    IEnumerator Attack()
    {
        //Attatches bullet to position of the shooter
        yield return new WaitForSeconds(Random.Range(1, 3));
        //Starts throwing animation
        myAnimator.SetBool("isAttacking", true);
        //Gives a little time for throwing animation to start
        yield return new WaitForSeconds(0.5f);
        //Takes bullet object and is making it active on the screen
        Instantiate(bullet, new Vector2(transform.position.x + bulletXDistance, transform.position.y + bulletYDistance), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        myAnimator.SetBool("isAttacking", false);
        //Begins the coroutine
        StartCoroutine(Attack());    
    }    

}
