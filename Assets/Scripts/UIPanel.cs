using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(CanvasGroup))]
public class UIPanel : MonoBehaviour
{
    protected CanvasGroup cg;

    public void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public void Appear(bool instant=false)
    {
        cg.blocksRaycasts = true;
        gameObject.SetActive(true);
        enabled = true;
        OnAppearStart();

        if (instant)
        {
            cg.alpha = 1f;
            cg.interactable = true;
        }
        else
        {
            cg.DOFade(1f, 0.25f).OnComplete(() =>
            {
                cg.interactable = true;
                OnAppearEnd();
            });
        }
    }
    protected virtual void OnAppearStart()
    {

    }
    protected virtual void OnAppearEnd()
    {
        //this wont shoot if appearing is instant
    }


    public void Disappear(bool instant=false)
    {
        cg.blocksRaycasts = false;
        OnDisappearStart();

        if (instant)
        {
            cg.alpha = 0f;
            cg.interactable = false;
        }
        else
        {
            cg.DOFade(0f, 0.25f).OnComplete(() =>
            {
                cg.interactable = false;
                OnDisappearEnd();
            });
        }
    }
    protected virtual void OnDisappearStart()
    {
    }
    protected virtual void OnDisappearEnd()
    {
        //this wont shoot if disappearing is instant
    }

}
