using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MenuPanel;
    private GameObject InGameMenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            InGameMenuPanel = GameObject.Find("InGameMenu");
            InGameMenuPanel.SetActive(true);
            MenuPanel.SetActive(false);
        }
        else { MenuPanel.SetActive(true);}
    }
    public void ShowLevelPanel()
    {
        MenuPanel.SetActive(false);
        InGameMenuPanel.SetActive(true);
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
        InGameMenuPanel.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
