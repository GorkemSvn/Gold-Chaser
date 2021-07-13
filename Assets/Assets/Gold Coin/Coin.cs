using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public float turnSpeed;
    public float collectionSpeed=1f;
    public Transform uiTarget; //set this at instantiation
    public OnCollectedCallBack onCollected;
    public int value = 10;
    public float lifeLenght = 10f;

    void FixedUpdate()
    {
        transform.RotateAroundLocal(Vector3.up, turnSpeed * Time.fixedDeltaTime);
    }

    public void OnEnable()
    {
        Vector3 startVector = transform.localScale;
        transform.localScale = Vector3.zero;
        transform.DOScale(startVector, 0.2f);
        StartCoroutine(Life());
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
            StartCoroutine(MoveTo(uiTarget, collectionSpeed));
            GetComponent<Collider>().enabled = false;
        
    }
    IEnumerator Life()
    {
        yield return new WaitForSeconds(lifeLenght);
        transform.DOScale(0, 0.4f).OnComplete(()=>gameObject.SetActive(false));
    }
    IEnumerator MoveTo(Transform target,float tl)
    {
        Camera cam = Camera.main;
        transform.SetParent(cam.transform);
        if (target != null)
        {
            Vector3 prePos = target.position;
            prePos.z = 10;
            Vector3 localpos = cam.transform.InverseTransformPoint(cam.ScreenToWorldPoint(prePos));
            transform.DOLocalMove(localpos, tl);
        }
        yield return new WaitForSeconds(tl);

        transform.DOScale(0.03f, tl);


        transform.DOScale(0, 0.2f);
        onCollected?.Invoke(value);
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    public delegate void OnCollectedCallBack(int value);
}
