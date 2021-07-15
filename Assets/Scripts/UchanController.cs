using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UchanController : MonoBehaviour
{
    public float moveSpeed=2f;
    public float accerelation=1f;
    public bool point;
    Animator animator;
    Vector3 moveDirection;
    int moveSpeedId;
    Coin detectedGoldCoin;
    CapsuleCollider capsuleCollider;
    bool onGround;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameManager.instance.UChanRegister(this);
        moveSpeedId = Animator.StringToHash("MoveSpeed");
        GrandManager.finish += OnEnd;
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        onGround = CheckGround();
        float sqrm = moveDirection.sqrMagnitude;

        animator.SetFloat(moveSpeedId, sqrm);

        if (sqrm > 0.05f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.position += moveDirection * moveSpeed * Time.fixedDeltaTime;
        }

        animator.SetBool("Airborn", !onGround);
    }

    public void OnTriggerEnter(Collider other)
    {
        Coin c = other.GetComponent<Coin>();
        if (c)
            detectedGoldCoin = c;
    }

    public void Control(Vector3 worldDirection)
    {
        if (onGround)
        {
            moveDirection = Vector3.Lerp(moveDirection, worldDirection, Time.fixedDeltaTime * accerelation);
        }
    }

    bool CheckGround()
    {
        Vector3 origin = transform.position + Vector3.up * (capsuleCollider.radius + Physics.defaultContactOffset);
        float distance = capsuleCollider.radius + Physics.defaultContactOffset;

        return Physics.SphereCast(origin,capsuleCollider.radius, -Vector3.up, out RaycastHit hit,distance);
    }
    void OnEnd()
    {
        moveDirection = Vector3.zero;
        if (GameManager.instance.win)
        {
            animator.SetTrigger("Win");
            //transform.position = Vector3.zero;
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else
            animator.SetTrigger("Lose");

        point = false;
        GrandManager.finish -= OnEnd;
    }
    public void OnAnimatorIK()
    {
        if (!point)
            return;

        if (detectedGoldCoin)
        {
            Vector3 localpos = transform.InverseTransformPoint(detectedGoldCoin.transform.position);
            if (localpos.z > 1f)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKPosition(AvatarIKGoal.RightHand, detectedGoldCoin.transform.position);
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