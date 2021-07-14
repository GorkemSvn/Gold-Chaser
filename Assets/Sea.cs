using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea : MonoBehaviour
{
    public ParticleSystem ps1, ps2;
    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        Rigidbody r=other.GetComponent<Rigidbody>();
        if (r)
        {
            r.drag = 10f;
            r.constraints = RigidbodyConstraints.None;
        }

        Vector3 pos = GetComponent<Collider>().ClosestPointOnBounds(r.position);
        ps1.transform.parent.position = pos;
        ps1.Play();
        ps2.Play();

        if (r.GetComponent<UchanController>())
        {
            GameManager.instance.ReportEnd(false);
        }
    }
}
