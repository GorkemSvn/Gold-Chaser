using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacingCamera : MonoBehaviour
{    
    public static RacingCamera instance;
    public Vector3 offset;
    public float distance = 2;

    public float horizontalAngle;
    float horizontalR;
    public float verticalAngle;
    float verticalR;

    public bool smoothPosition, localAngle;
    public Transform target;

    public void Awake()
    {
        instance = this;
        SetHorizontalAngle(horizontalAngle);
        SetVerticalAngle(verticalAngle);
    }

    public void FixedUpdate()
    {
        if (target)
        {
            Vector3 cameraDir = CameraDirection();

            Vector3 targetPos;
            if (localAngle)
                targetPos = target.position + target.TransformDirection(cameraDir) * distance + offset;
            else
                targetPos = target.position + cameraDir * distance + offset;

            if (smoothPosition)
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.fixedDeltaTime*12f);
            else
                transform.position = targetPos;

            transform.GetChild(0).LookAt(target.position);
        }
    }

    Vector3 CameraDirection()
    {
        float horizontalDistance = Mathf.Cos(verticalR);

        Vector3 direction=new Vector3(
        horizontalDistance * Mathf.Sin(horizontalR),
        Mathf.Sin(verticalR),
        horizontalDistance * Mathf.Cos(horizontalR));

        return direction;
    }

    static float DegreeToRadian(float degree)
    {
        return degree * Mathf.PI / 180f;
    }

    public void SetHorizontalAngle(float degree)
    {
        horizontalAngle = degree;
        horizontalR = DegreeToRadian(degree);
    }
    public void SetVerticalAngle(float degree)
    {
        verticalAngle = degree;
        verticalR = DegreeToRadian(degree);
    }
    public void OnValidate()
    {
        SetHorizontalAngle(horizontalAngle);
        SetVerticalAngle(verticalAngle);
    }

    [System.Serializable]
    public class Transition
    {
        public Vector3 offset;
        public float distance = 2;

        public float horizontalAngle;
        public float verticalAngle;

        public bool smoothPosition, localAngle;
        public Transform target;

        public void Transite()
        {
            instance.offset = offset;
            instance.distance = distance;
            instance.SetHorizontalAngle(horizontalAngle);
            instance.SetVerticalAngle(verticalAngle);
            instance.smoothPosition = smoothPosition;
            instance.localAngle = localAngle;
            instance.target = target;
        }
    }
}
