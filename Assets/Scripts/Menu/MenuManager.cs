using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject CheatBtn;
    private bool isCheatActive = false;

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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCheatActive) ShowCheatButton(isCheatActive);
            else ShowCheatButton(isCheatActive);
        }

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

    public void ShowCheatButton(bool state)
    {
        CheatBtn.SetActive(state);
        isCheatActive = !isCheatActive;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Destroy(GameObject.Find("ParamStart"));
        SceneManager.LoadScene(0);
    }
}
