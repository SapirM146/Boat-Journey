using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndMenuScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // unlock cursor
        Cursor.visible = true; // show curser
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(1); // return to first level
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
