using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBody : MonoBehaviour
{
    //Accesses the player
    private GameObject player;
    //Accesses the player's health
    private float playerHealth;
    //Accesses the amount the player loses its health by
    private float playerHealthBurn;
    //Accesses the players healthbar
    private Slider playerHealthBar;
    // Start is called before the first frame update
    private GameObject boss;
    //Checks if player is hit
    void Start()
    {
        player = GameObject.Find("Player");
        playerHealthBurn = player.GetComponent<PlayerScript>().healthBurn;
        boss = GameObject.Find("Stapler Idle");

    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "player")
        {
            if (!player.GetComponent<PlayerScript>().invincible)
            {
                player.GetComponent<PlayerScript>().health -= playerHealthBurn;
                player.GetComponent<PlayerScript>().healthBar.value = player.GetComponent<PlayerScript>().health;
            }         
            boss.GetComponent<EnemyBoss>().hitPlayer = true;
        }
            
    }

    

}
