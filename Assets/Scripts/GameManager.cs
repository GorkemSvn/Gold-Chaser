using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * GameManager manages in-level events and takes reports from in-level objects
 * holds temporary(in level) data, does not edit Grand Data
 * GameManager works with grand manager 
 */
public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    Coroutine characterControlProcess;

    public bool win { get; private set; }
    public int collectedGold { get; private set; }

    public int debugLevel;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            GrandManager.Data.Load();
            SceneManager.sceneLoaded += OnLoad;
        }
        else
            Destroy(gameObject);
    }

    public void Start()
    {
        if (debugLevel > 0)
            GrandManager.LevelManager.LoadLevel(debugLevel);
        else
            GrandManager.LevelManager.LoadLevel(GrandManager.Data.playerLevel);
    }


    public void OnLoad(Scene scene, LoadSceneMode lsm)
    {
        collectedGold = 0;
        win = false;
        GrandManager.CallStatusUpdate();


    }

    public void UChanRegister(UchanController uchan)
    {
        if (characterControlProcess != null)
        {
            StopCoroutine(characterControlProcess);
            characterControlProcess = StartCoroutine(CharacterControl(uchan));
        }
        else
            characterControlProcess = StartCoroutine(CharacterControl(uchan));
    }

    IEnumerator CharacterControl(UchanController uchan)
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Got uchan");
        ButtonJoystick joystick = (UIManager.instance.gamePanel as GamePanel).joystick;

        while (enabled)
        {
            if (joystick.swiping)
                uchan.Control(joystick.HorizontalDirection);
            else
                uchan.Control(Vector3.zero);
            yield return new WaitForFixedUpdate();
        }
    }

    public void GetGold(int q)
    {
        collectedGold += q;
        GrandManager.CallStatusUpdate();
    }

    public void ReportEnd(bool win)
    {
        this.win = win;
        GrandManager.CallFinishEvent();
    }
}


