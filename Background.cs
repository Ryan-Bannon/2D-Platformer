using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject player;
    private Transform playerTransform;
    private Vector3 lastPlayerPosition;
    [SerializeField]
    private Vector2 parallaxEffectMultiplier;
    
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
        lastPlayerPosition = playerTransform.position;
       
    }

   
    void LateUpdate()
    {
        if(player != null && player.GetComponent<PlayerScript>().isAlive )
        {
            Vector3 deltaMovement = playerTransform.position - lastPlayerPosition;
            transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
            lastPlayerPosition = playerTransform.position;
        }        
    }
}
