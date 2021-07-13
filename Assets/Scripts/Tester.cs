using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    public void End(bool win)
    {
        GameManager.instance.ReportEnd(win);
    }

    public void CollectGold()
    {
        GameManager.instance.GetGold(Random.Range(1,5));
    }
}
