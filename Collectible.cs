using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collectible : MonoBehaviour
{
    public float currentPos;
    // Start is called before the first frame update
    void Start()
    {
        if (Door.instance != null)
            Door.instance.collectiblesCount++;
    }

    // Update is called once per frame
    void Update()
    {
       // currentPos = transform.position.y;
       // if (currentPos <= -5)
        //    Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "player")
        {
            Destroy(gameObject);
            if (Door.instance != null)
                Door.instance.DecrementCollectibles();
        }
    }
}
