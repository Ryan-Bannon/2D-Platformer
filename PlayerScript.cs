using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public static PlayerScript instance;
    private Rigidbody2D myRigidbody;
    //private BoxCollider2D myBoxCollider;
    public float moveSpeed;
    private bool facingRight;
    private Animator myAnimator;
    //private PhysicsMaterial2D playerMaterial;


    [SerializeField]
    private Transform[] groundPoints; //Creates an array of "points" (actually game objects to collide with the ground
    [SerializeField]
    private float groundRadius; //Creates the size of the colliders
    [SerializeField]
    private LayerMask whatIsGround;//Defines what is ground
    
    private bool isGrounded; //Can be set to false based on our position
    private bool isJumping;//can be set to true or false when we press the space key

    [SerializeField]
    private float jumpForce;

    public bool isAlive;

    private float originalPos;

    public bool canJump;

    public GameObject reset;

    public GameObject nextLevel;

    private float firstPos;

    public float verticalPos;

    public float horizontal;

    //public bool headIsTouching;

    public Slider healthBar;

    public float health = 3f;

    public float healthBurn = 1f;

    private bool pressedKey;

    private bool pressedKeyDown;

    private float velocity;

    //Determines whether the player can take damage
    public bool invincible;

    //Is used to check if the player has lost health
    private float originalHealth;
    //Checks if player is hit
    private bool hit;


 


    



  

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();    //a variable to control the Player's body
        myAnimator = GetComponent<Animator>();      //a variable to control the player's animator controller
        reset.SetActive(false);
        isAlive = true;
        //headIsTouching = false;
        healthBar = GameObject.Find("healthBar").GetComponent<Slider>();
        healthBar.minValue = 0f;
        healthBar.maxValue = health;
        healthBar.value = healthBar.maxValue;
        if(nextLevel != null)
            nextLevel.SetActive(false);
        originalHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = IsGrounded();
        horizontal = Input.GetAxis("Horizontal"); // a variable that stores the value of our horizontal movement
        //float vertical = Input.GetAxis("Vertical");
        //Debug.Log("Horizontal: " + horizontal + "Other one: " + transform.position.x);
        verticalPos = transform.position.y;
        if (health <= 0)
            isAlive = false;
        if (verticalPos <= -10)
        {
            Die();
            Destroy(gameObject);
            reset.SetActive(true);
        }
          
        if (isAlive)
        {
           // myBoxCollider.sharedMaterial.friction = 0f;
            PlayerMovement(horizontal);
            Flip(horizontal);
            HandleInput();

        }
        else
        {
            myAnimator.SetBool("dead", true);
            reset.SetActive(true);
            return;
        }
        //Checks if the health changes, and if it does, the player is invincible
        if (health != originalHealth )
        {
            invincible = true;
            originalHealth = health;
            Debug.Log("It Worked!");
            hit = true;
        }
        //Checks if player is invincible and was hit, and if it was, then it makes it invincible
        if (invincible && hit)
            StartCoroutine(IsInvincible());
        //Sets player being hit to false
        if (health == originalHealth)
            hit = false;
             
        

    }
    
    

    //Function Definitions

    //a function that controls player on the x-axis
    private void PlayerMovement(float horizontal)
    {
        if (isGrounded && isJumping)
        {           
            isGrounded = false;
            isJumping = false;
            //myRigidbody.AddForce(new Vector2(0, jumpForce));
        }
        myRigidbody.velocity = new Vector2(horizontal * moveSpeed, myRigidbody.velocity.y); //adds velocity to the player's body on the x-axis
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
        


    }

    private void Flip(float horizontal)
    {
        if ( (horizontal < 0 && facingRight) || (horizontal > 0 && !facingRight) )
        {
            facingRight = !facingRight; //rests the bool to the opposite value
            Vector2 theScale = transform.localScale; //creating a vector 2 and storing the local scale values
            theScale.x *= -1; //Flipping the x value of the scale
            transform.localScale = theScale;
        }
    }

    private void HandleInput()
    {
      
        //Debug.Log("Player's Y Velocity Not Jumping: " + myRigidbody.velocity.y);
        if (isGrounded)
        {
            myAnimator.SetBool("jumping", false); //Stops jumping animation if player is touching ground
            originalPos = transform.position.y; //Sets the original postion to where the player is when touching a platform
            canJump = true;        
            velocity = myRigidbody.velocity.y;
            


        }
        else
        {
            myAnimator.SetBool("jumping", true); //If the player is not touching the ground, the jump animation will play
            
        }
        //Checks if the user pressed a jump key
        pressedKeyDown = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        if (pressedKeyDown && canJump)
            canJump = false;
        
        float currentPos = transform.position.y; //Tracks current y position of player
        float jumpHeight = currentPos - originalPos; //Calculates how much the player has moved from their current position

        
        float maxHeight = jumpForce / 2; //Determines the maximum height the player can jump relative to their decided jump height 

        //Checks if the player is over the maxiumum height they are allowed to jump
        if (jumpHeight > maxHeight)
        {
            //Makes it so the player can no longer move upwards
            canJump = false;
            //myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
            //Debug.Log("TOO HIGH!!!!!");
        }
        //Checks if the user pressed a jump key
        pressedKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
        if (pressedKey && canJump) //Checks if the user is still holding the space bar and is not above the maxHeight
        {
            //myRigidbody.velocity = Vector2.up * jumpForce; //Increases the player's velocity upwards

            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, velocity + jumpForce);
            if (!isGrounded)
                myAnimator.SetBool("jumping", true);
            else
                myAnimator.SetBool("jumping", false);
            // Debug.Log("Player's Y Velocity While Jumping: " + myRigidbody.velocity.y);
        }
        
            //Debug.Log("NO LONGER CAN JUMP!!!");
       

        //else if(!canJump)
        // myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);



    }
    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            //if the player is not moving vertically, test each of the Player's groundPoints for collision with Ground
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; 1 < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject) //if any of the colliders in the array of groundPoints comes into contact with another gameobject, return true.
                    {
                        //headIsTouching = false;
                        return true;
                    } 
                       
                }
            }
        }
        return false; //if the player is not moving along the y axis, return false.
    }
    private void Die()
    {
        health = 0;
        healthBar.value = 0;
        isAlive = false;
        Destroy(gameObject);
    }

    IEnumerator IsInvincible()
    {
        myAnimator.SetBool("invincible", true);
        yield return new WaitForSeconds(1);
        invincible = false;
        myAnimator.Rebind();
        StopCoroutine(IsInvincible());
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "ground")
        {
            myAnimator.SetBool("jumping", false);
            canJump = false;
        }

        if ((target.gameObject.tag == "deadly" || target.gameObject.tag == "shooter") && !invincible)
        {
            Debug.Log("InvincibleBeforeChange: " + invincible);
            health -= healthBurn;
            healthBar.value = health;
            //invincible = true;
            
        }
    }




}
/*
 *  private void HandleInput()
    {

        float currentPos = transform.position.y; //Tracks current y position of player
        
        if (isGrounded)
        {
            myAnimator.SetBool("jumping", false); //Stops jumping animation if player is touching ground
            originalPos = transform.position.y; //Sets the original postion to where the player is when touching a platform
            canJump = true;
            isJumping = false;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
        }
        else
        {
            myAnimator.SetBool("jumping", true); //If the player is not touching the ground, the jump animation will play
            
        }
        //Checks if the user pressed a jump key
        pressedKeyDown = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow);
        float jumpHeight = currentPos - originalPos; //Calculates how much the player has moved from their current position
        if (pressedKeyDown && canJump)
        {
            //Debug.Log("1 is WORKING");
            isJumping = true;
            myAnimator.SetBool("jumping", true);
            canJump = false;
        }
        float maxHeight = jumpForce / 2; //Determines the maximum height the player can jump relative to their decided jump height 

        //Checks if the player is over the maxiumum height they are allowed to jump
        if (jumpHeight > maxHeight)
        {
            //Makes it so the player can no longer move upwards
            canJump = false;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
        }
        //Checks if the user pressed a jump key
        pressedKey = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow);
        if (pressedKey && canJump) //Checks if the user is still holding the space bar and is not above the maxHeight
        {
            //myRigidbody.velocity = Vector2.up * jumpForce; //Increases the player's velocity upwards
            Debug.Log("Jump Force: " + jumpForce);
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }
        //else if(!canJump)
           // myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
        


    } */




