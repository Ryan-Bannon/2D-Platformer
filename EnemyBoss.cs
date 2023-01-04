using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    //Will be used to help the enemy change direction when it encounters the edge of the ground
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private Transform startPos, endPos;
    //Will check for ground
    private bool collision;
    //Allows the enemy to walk on the ground
    public float speed = 2f;
    //Enemy's health
    public float health = 5f;
    //Amount of health enemy loses when hit
    public float healthBurn = 1f;
    
    [SerializeField]
    private GameObject staple;

    
    [SerializeField]
    private float stapleXDistance;

    public float stapleSpeed;


    public bool hitPlayer;

    public GameObject winButton;

   

   

    void Start()
    {
        StartCoroutine(Attack());
        
    }

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }
   
    //Update is called once per frame
    void FixedUpdate()
    {
        Move();
        ChangeDirection();

        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log("WORKED");
            winButton.SetActive(true);
        }
            
    }

    void Move()
    {
        myRigidbody.velocity = new Vector2(transform.localScale.x, 0) * speed;
    }
    
    

    public void ChangeDirection()
    {
        collision = Physics2D.Linecast( startPos.position, endPos.position, 1 << LayerMask.NameToLayer("ground") );

        if (!collision || hitPlayer)
        {
            Vector3 temp = transform.localScale;

            if (temp.x == 1)
                temp.x = -1f;
            else
                temp.x = 1f;
                
            transform.localScale = temp;
            hitPlayer = false;
        }
    }
    
    IEnumerator Attack()
    {

        float originalXDistance = stapleXDistance;
        //Attatches bullet to position of the shooter
        yield return new WaitForSeconds(Random.Range(0.5f, 1));
        
        //Checks which way the staple will fire
        if (transform.localScale.x < 0)
            stapleXDistance *= -1;
        //Takes bullet object and is making it active on the screen
        Instantiate(staple, new Vector2(transform.position.x + stapleXDistance, transform.position.y), Quaternion.identity);
        stapleXDistance = originalXDistance;
        //yield return new WaitForSeconds(0.1f);
        //Begins the coroutine
        StartCoroutine(Attack());
    }


}
