using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJumper : MonoBehaviour
{
    public float forceY = 300f;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D> ();
        myAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine ( Attack() );
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(2, 4));
        myAnimator.SetBool("attack", true);
        forceY = Random.Range(300, 500);
        myRigidbody.AddForce(new Vector2(0, forceY));
        yield return new WaitForSeconds(1.5f);
        myAnimator.SetBool("attack", false);
        StartCoroutine( Attack() );
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "bullet")
        {
            Destroy (gameObject);
            Destroy (target.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
