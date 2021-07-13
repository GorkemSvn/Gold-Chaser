using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinSpawner : MonoBehaviour
{
    public Pool coinPool;
    public float period = 3f;
    public float width,height, lenght;
    public void Start()
    {
        if (GameManager.instance == null)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);

        coinPool.Instantiate();
        StartCoroutine(spawningProcess());
        GrandManager.finish += onFinish;
    }

    void onFinish()
    {
        enabled = false;
        GrandManager.finish -= onFinish;
    }
    IEnumerator spawningProcess()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(period);
            GameObject coin= coinPool.Spawn(RandomPosition());
            Coin c = coin.GetComponent<Coin>();
            c.uiTarget = (UIManager.instance.gamePanel as GamePanel).goldText.transform.parent;
            c.onCollected = GameManager.instance.GetGold;
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 pos = Vector3.zero;
        pos.x = Random.Range(-width, width);
        pos.y = height;
        pos.z = Random.Range(-lenght, lenght);
        return pos;
    }
}
