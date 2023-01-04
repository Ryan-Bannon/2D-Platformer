using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadScript : MonoBehaviour
{
    public GameObject player;
   
    void Start()
    {
        player = GameObject.Find("Player");
        
    }

   
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        player.GetComponent<PlayerScript>().canJump = false;
        /*if (target.gameObject.tag == "deadly" || target.gameObject.tag == "shooter")
        {
            player.GetComponent<PlayerScript>().health -= player.GetComponent<PlayerScript>().healthBurn;
            player.GetComponent<PlayerScript>().healthBar.value = health;
            Debug.Log("Health: " + health + " HealthBurn: " + healthBurn);
        }*/
    }
}
