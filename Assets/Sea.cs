using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        Rigidbody r=other.GetComponent<Rigidbody>();
        if (r)
            r.drag = 5f;

        if (r.GetComponent<UchanController>())
        {
            GameManager.instance.ReportEnd(false);
        }
    }
}
