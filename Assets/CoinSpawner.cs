using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public Pool coinPool;
    public float period = 3f;
    IEnumerator spawningProcess()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(period);
            //spawn at random poisiton here;
        }
    }
}
