using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public UIPanel startPanel;
    public UIPanel gamePanel;
    public UIPanel pausePanel;
    public UIPanel finalPanel;

    public void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;

            startPanel.gameObject.SetActive(true);
            gamePanel.gameObject.SetActive(true);
            pausePanel.gameObject.SetActive(true);
            finalPanel.gameObject.SetActive(true);

            DontDestroyOnLoad(gameObject);

            GrandManager.finish += EndGame;
            GrandManager.load += GreetGame;
        }
    }
    
    private void GreetGame()
    {
        OpenPanel(startPanel);
    }

    public void StartGame()
    {
        //this is called by start panel sc
        OpenPanel(gamePanel);
        GrandManager.CallStartEvent();
    }

    public void PauseGame()
    {
        //this is called by game panel sc
        OpenPanel(pausePanel);
    }

    private void EndGame()
    {
        OpenPanel(finalPanel);
    }

    private void OpenPanel(UIPanel panel)
    {
        startPanel.Disappear(true);
        gamePanel.Disappear(true);
        pausePanel.Disappear(true);
        finalPanel.Disappear(true);

        panel.Appear();
    }
}
//The ui should take player's input and send commands to the game according to it
//Also ui should observe the game and display information for the player