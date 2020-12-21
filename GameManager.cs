using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float gameTime;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameTime += Time.deltaTime;
    }
    public void end()
    {
        SceneManager.LoadScene("Results");
    }
    public void reset()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public void winGame()
    {
        SceneManager.LoadScene("WinPage");
    }
}