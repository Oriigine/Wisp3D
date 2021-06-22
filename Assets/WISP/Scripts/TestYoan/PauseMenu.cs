using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject m_PauseMenu = null;

    [SerializeField]
    private XboxMapping m_XboxMapping;

    [SerializeField]
    GameObject m_PauseFirstButton;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || m_XboxMapping.menuButton)
        {
            Pause();
        }
    }

    public void Menu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Go to the scene game");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGame");
        Debug.Log("Go to the scene game");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        m_PauseMenu.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        m_PauseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("quit");
    }
}
