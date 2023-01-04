using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BossHead : MonoBehaviour
{
    //The boss's gameobject
    private GameObject boss;
    //The player's gameobject
    private GameObject player;
    //The boss's health
    private float bossHealth;
    //The amount the boss's health will decrease by
    private float bossHealthBurn;
    //Boss's healthbar
    private Slider bossHealthBar;
    //Boss's Speed
    private float bossSpeed;
    //Boss's Staple Speed
    private float stapleSpeed;
    //Controls the text that appears when you kill the boss
    public TMP_Text winText;

    public GameObject replayButton;

    public GameObject shooter1, shooter2, shooter3, shooter4, shooter5;

    private int counter;

    //Determines whether the boss can take damage
    public bool invincible;
    
    //Checks if player is hit
    private bool hit;

    private Animator myAnimator;

    public GameObject jumpBarrier;

    public GameObject playerBottom;

    public GameObject winButton;

    void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.Find("Stapler Idle");
        bossHealth = boss.GetComponent<EnemyBoss>().health;
        bossHealthBurn = boss.GetComponent<EnemyBoss>().healthBurn;
        bossHealthBar = GameObject.Find("bossHealthBar").GetComponent<Slider>();
        bossHealthBar.minValue = 0f;
        bossHealthBar.maxValue = bossHealth;
        bossHealthBar.value = bossHealthBar.maxValue;
        bossSpeed = boss.GetComponent<EnemyBoss>().speed;
        stapleSpeed = boss.GetComponent<EnemyBoss>().stapleSpeed;
        winText.text = "";
        myAnimator = boss.GetComponent<Animator>();

    }
    void Update()
    {
        
        bossSpeed = boss.GetComponent<EnemyBoss>().speed;
        if (bossHealth == 8)
        {
            if(counter == 0)
            {
                UpdateBoss();
            }
              
            shooter1.SetActive(true);
        }
            
        else if (bossHealth == 6)
        {
            if(counter == 1)
                UpdateBoss();
            shooter5.SetActive(true);
        }
            
        else if (bossHealth == 4)
        {
            if(counter == 2)
                UpdateBoss();
            shooter2.SetActive(true);
        }
            
        else if (bossHealth == 2)
        {
            shooter4.SetActive(true);
            shooter3.SetActive(true);
        }

        else if (bossHealth <= 0)
            Die();
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "player" && !invincible && (playerBottom.transform.position.y > jumpBarrier.transform.position.y) )
        {
            bossHealth -= bossHealthBurn;
            bossHealthBar.value = bossHealth;
            invincible = true;
            StartCoroutine(IsInvincible());
        }
    }
    private void Die()
    {
        winButton.SetActive(true);
        Destroy(boss);
        winText.text = "YOU WIN!!!";
        replayButton.SetActive(false);
        shooter1.SetActive(false);
        shooter5.SetActive(false);
        shooter2.SetActive(false);
        shooter4.SetActive(false);
        shooter3.SetActive(false);
    }
    private void UpdateBoss()
    {

        boss.GetComponent<EnemyBoss>().speed *= 1.25f;
        counter++;
    }
    IEnumerator IsInvincible()
    {
        myAnimator.SetBool("invincible", true);
        yield return new WaitForSeconds(1);
        invincible = false;
        myAnimator.SetBool("invincible", false);
        StopCoroutine(IsInvincible());
    }
}
