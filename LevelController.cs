using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class LevelController : MonoBehaviour
{
    public GameObject nextLevel;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Lvl1()
    {
        SceneManager.LoadScene("LVL 1");
    }

    public void Lvl2()
    {
        SceneManager.LoadScene("LVL 2");
    }
  
}
