using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
        Debug.Log("Go to the scene game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
