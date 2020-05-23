using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public Image hp_bar;
    public static BoatHPScript playerBoat;

    private void Start()
    {
        playerBoat = GameObject.FindGameObjectWithTag("Player").GetComponent<BoatHPScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float hp = playerBoat.getBoatHP();
        hp_bar.fillAmount = hp;
        
        if (hp <= 0)
            gameOver();
    }

    public static void gameWin()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
