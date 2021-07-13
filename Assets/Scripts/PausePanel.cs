using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PausePanel : UIPanel
{
    protected override void OnAppearEnd()
    {
        Time.timeScale = 0f;
    }
    protected override void OnDisappearStart()
    {
        Time.timeScale = 1f;
    }

    public void Continue()
    {
        UIManager.instance.StartGame();
    }

    public void Retry()
    {
        //show ads here
        GrandManager.LevelManager.LoadLevel(GrandManager.LevelManager.activeLevel);
    }
}
