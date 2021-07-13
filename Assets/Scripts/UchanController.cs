using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UchanController : MonoBehaviour
{
    public float moveSpeed=2f;
    public float accerelation=1f;
    Animator animator;
    Vector3 moveDirection;
    int moveSpeedId;
    Coin goldCoin;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.instance.UChanRegister(this);
        moveSpeedId = Animator.StringToHash("MoveSpeed");
        GrandManager.finish += OnEnd;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float sqrm = moveDirection.sqrMagnitude;

        animator.SetFloat(moveSpeedId, sqrm);

        if (sqrm > 0.05f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        Coin c = other.GetComponent<Coin>();
        if (c)
            goldCoin = c;
    }

    public void Control(Vector3 worldDirection)
    {
        moveDirection = Vector3.Lerp(moveDirection, worldDirection,Time.fixedDeltaTime* accerelation);
    }
    void OnEnd()
    {
        transform.position = Vector3.zero;
        moveDirection = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(Vector3.forward);
        if (GameManager.instance.win)
            animator.SetTrigger("Win");
        else
            animator.SetTrigger("Lose");

        GrandManager.finish -= OnEnd;
    }
    public void OnAnimatorIK()
    {
        if (goldCoin)
        {
            Vector3 localpos = transform.InverseTransformPoint(goldCoin.transform.position);
            if (localpos.z > 1f)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, goldCoin.transform.position);
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
            }
        }
        else
        {
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0f);
        }
    }
}
