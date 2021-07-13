using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class EndPanel : UIPanel
{
    public GameObject retry, next;
    public Text title;

    protected override void OnAppearStart()
    {
        next.SetActive(GameManager.instance.win);

        if (GameManager.instance.win)
            title.text = "Success!";
        else
            title.text = "Failure!";

        title.color = Color.white;
        title.gameObject.SetActive(false);
    }
    protected override void OnAppearEnd()
    {
        title.transform.localScale = Vector3.one * 4;
        title.gameObject.SetActive(true);

        if (GameManager.instance.win)
        {
            title.DOFade(1f, 0.05f);
            title.DOColor(Color.green, 0.2f);
            title.transform.DOScale(2f, 0.2f);
        }
        else
        {
            title.DOFade(1f, 0.05f);
            title.DOColor(Color.red, 0.2f);
            title.transform.DOScale(2f, 0.2f);
        }
    }
    public void Retry()
    {
        //show ads here
        GrandManager.LevelManager.LoadLevel(GrandManager.LevelManager.activeLevel);
    }
    public void Next()
    {
        //show ads here
        GrandManager.LevelManager.LoadLevel(GrandManager.LevelManager.activeLevel+1);
    }
}
