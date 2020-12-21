using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonscripts : MonoBehaviour
{
    public Text PauseText;
    public GameObject PauseMenu;
    private static bool isPaused = false;
    public void Play()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Pause()
    {
        if(isPaused == false)
        {
            PauseText.text = "Continue";
            Time.timeScale = 0;
            isPaused = true;
            PauseMenu.SetActive(true);
        } else{
            Continue();
        }

    }
    public void Continue()
    {
        PauseText.text = "Pause";
        Time.timeScale = 1;
        isPaused = false;
        PauseMenu.SetActive(false);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("MenuPage");
    }
}
