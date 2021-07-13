using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public float turnSpeed;
    public float collectionSpeed=1f;
    public Transform target; //set this at instantiation
    public OnCollectedCallBack onCollected;

    void FixedUpdate()
    {
        transform.RotateAroundLocal(Vector3.up, turnSpeed * Time.fixedDeltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;

        else if (other.transform.root.CompareTag("Character"))
        {
            GetCollected();
        }
    }
    public void GetCollected()
    {
            StartCoroutine(MoveTo(target, collectionSpeed));
            GetComponent<Collider>().enabled = false;
        
    }
    IEnumerator MoveTo(Transform target,float tl)
    {
        Camera cam = Camera.main;
        if (target != null)
        {
            Vector3 localpos = cam.transform.InverseTransformPoint(cam.ScreenToWorldPoint(target.position));
            transform.DOLocalMove(localpos, tl);
        }

        transform.SetParent(cam.transform);
        transform.DOScale(0.03f, tl);

        yield return new WaitForSeconds(tl);

        transform.DOScale(0, 0.2f);
        onCollected?.Invoke();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    public delegate void OnCollectedCallBack();
}
