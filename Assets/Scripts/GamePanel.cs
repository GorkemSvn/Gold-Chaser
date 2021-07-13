using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : UIPanel
{
    public Text goldText;
    public ButtonJoystick joystick;

    public void Start()
    {
        GrandManager.update += UpdateGoldText;
    }

    protected override void OnAppearStart()
    {
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
}
