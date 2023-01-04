using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform; 
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && player.GetComponent<PlayerScript>().isAlive)
        {
            Vector3 temp = transform.position;
            temp.x = playerTransform.position.x;
            transform.position = temp;
        }
        
    }
}
