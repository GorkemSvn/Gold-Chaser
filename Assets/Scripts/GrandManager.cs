using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* this master class holds status information of game and manage major events such as win,lose,start,gold,in app purchases
* player's inventory,game configuration and other interlevel information
* also manages how to move acros levels
*/
public static class GrandManager
{
    public static event GameEvent load; //called by LevelManager
    public static event GameEvent start; //called by UIManager
    public static event GameEvent finish; //Called by GrandManager
    public static event GameEvent update; //Called by GameManager to notice UI (gold collection, damage taken)

    public static void CallStartEvent()
    {
        start?.Invoke();
    }
    public static void CallFinishEvent()
    {
        finish?.Invoke();
        if (GameManager.instance.win)
        {
            Data.playerLevel++;
            Data.gold += GameManager.instance.collectedGold;
            Data.Save();
        }
    }
    public static void CallStatusUpdate()
    {
        update?.Invoke();
    }

    public delegate void GameEvent();
    public static class Data
    {
        public static int gold;
        public static int playerLevel;
        public static bool playMusic=true;

        public static void Save()
        {
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.SetInt("playerLevel", playerLevel);

            if(playMusic)
                PlayerPrefs.SetInt("playMusic", 1);
            else
                PlayerPrefs.SetInt("playMusic", 0);
        }
        public static void Load()
        {
            gold = PlayerPrefs.GetInt("gold");
            playerLevel = PlayerPrefs.GetInt("playerLevel", 1);
            playMusic = PlayerPrefs.GetInt("playMusic",1) == 1;
        }
    }
    public static class LevelManager
    {
        public static int activeLevel;

        public static void LoadLevel(int level)
        {
            if (SceneManager.sceneCountInBuildSettings < 2)
            {
                Debug.LogError("Template needs project to have atleast 2 scenes in build to work");
                return;
            }

            level = Mathf.Max(1, level);
            int scene = 1 + (level - 1) % (SceneManager.sceneCountInBuildSettings - 1);

            activeLevel = level;
            Debug.Log("Loading Level " + level + " and scene " + scene);
            SceneManager.LoadScene(scene);
            load?.Invoke();
        }
    }
}
