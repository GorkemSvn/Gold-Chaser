using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : UIPanel
{
    public Text levelText, goldText;
    public RectTransform settingsPanel,settingsButton;
    public Image musicButton;
    bool settingsOpen,settingsMoving=false;

    protected override void OnAppearStart()
    {
        levelText.text = "Level " + GrandManager.LevelManager.activeLevel;
        goldText.text = "" +GrandManager.Data.gold;

        if (GrandManager.Data.playMusic)
            musicButton.color = Color.white;
        else
            musicButton.color = Color.grey;
    }

    public void StartGame()
    {
        //check to see if ads are available, dont start if not
        UIManager.instance.StartGame();
    }

    public void Settings()
    {
        if (settingsMoving)
            return;

        if (!settingsOpen )
        {
            settingsMoving = true;
            settingsButton.DORotate(-Vector3.forward *179, 1f);
            settingsPanel.DOMoveX(Screen.width-settingsPanel.rect.width/2f, 1f).OnComplete(() => { settingsOpen = true; settingsMoving = false; });
        }
        else
        {
            settingsMoving = true;
            settingsButton.DORotate(Vector3.forward *0, 1f);
            settingsPanel.DOMoveX(Screen.width +settingsPanel.rect.width/2f, 1f).OnComplete(() => { settingsOpen = false; settingsMoving = false; });
        }
    }
    public void ReverseMusic()
    {
        GrandManager.Data.playMusic = !GrandManager.Data.playMusic;
        GrandManager.Data.Save();

        if (GrandManager.Data.playMusic)
            musicButton.color = Color.white;
        else
            musicButton.color = Color.grey;
    }
}
