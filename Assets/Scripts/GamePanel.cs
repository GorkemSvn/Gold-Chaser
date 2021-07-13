using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : UIPanel
{
    public Text goldText;
    public ButtonJoystick joystick;
    public int countDown;
    public Text countDownText;

    int count;
    Coroutine countCo;

    public void Start()
    {
        GrandManager.update += UpdateGoldText;
        GrandManager.load += onLoad;
    }
    void onLoad()
    {
        if (countCo != null)
            StopCoroutine(countCo);

        countCo = null;
        count = countDown;
    }
    protected override void OnAppearStart()
    {
        if(countCo==null)
            countCo = StartCoroutine(CountDown());

        countDownText.text = "" + count;
        joystick.cam = RacingCamera.instance.GetComponentInChildren<Camera>();
        Debug.Log("registered camera");
        UpdateGoldText();
        base.OnAppearStart();
    }
    public void Pause()
    {
        UIManager.instance.PauseGame();
    }
    
    public void UpdateGoldText()
    {
        goldText.text = "" + GameManager.instance.collectedGold;
    }

    IEnumerator CountDown()
    {
        count=countDown;
        while(enabled)
        {
            yield return new WaitForSeconds(1f);
            countDownText.text = ""+--count;

            if (count < 1)
            {
                GameManager.instance.ReportEnd(false);
                break;
            }
        }

    }
}
