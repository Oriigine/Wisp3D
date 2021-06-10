using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_MainMenuFirstButton;

    public void Play()
    {
        //Clear Selected Button
        EventSystem.current.SetSelectedGameObject(null);
        //Set new selected Button
        EventSystem.current.SetSelectedGameObject(m_MainMenuFirstButton);


        SceneManager.LoadScene("MainScene");
        Debug.Log("Go to the scene game");

    }

    public void QuitGame()
    {
        //Clear Selected Button
        EventSystem.current.SetSelectedGameObject(null);

        Application.Quit();
        Debug.Log("quit");
    }
}
