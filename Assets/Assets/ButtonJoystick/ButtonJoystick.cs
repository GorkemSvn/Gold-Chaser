using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonJoystick : MonoBehaviour
{
    public Vector2 Direction { get; private set; }
    public Vector3 HorizontalDirection { get; private set; }
    public float sqrm { get; private set; }
    public bool swiping { get; private set; }

    float radius = 400;
    RectTransform rectTransform;
    public Camera cam;
    Transform circle,joystick;
    Vector2 startpos;
    int fingerID;

    void Start()
    {
        //referencing
        circle = transform.GetChild(0);
        joystick = transform.GetChild(0).GetChild(0);
        radius = circle.GetComponent<RectTransform>().sizeDelta.x / 2;
        startpos = circle.position;
        rectTransform = transform.GetComponent<RectTransform>();
        Disappear();
    }

    private void Update()
    {
        if(swiping)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (Input.GetTouch(i).fingerId == fingerID)
                {
                    Prosedure(Input.GetTouch(i).position);
                    return;
                }
            }

            Prosedure(Input.mousePosition);
        }

    }
    void Prosedure(Vector2 position)
    {
        Direction = (position - startpos) / radius;
        sqrm = Direction.sqrMagnitude;

        if (sqrm > 1)
        {
            Direction = Direction.normalized;
            sqrm = 1;
        }
        
        joystick.position = startpos + Direction * radius;

        if (cam != null)
        {
            Vector3 wdir = cam.transform.TransformDirection(Direction);
            wdir.y = 0;
            wdir.Normalize();
            HorizontalDirection = wdir;
        }
    }
    public Vector3 RelativeDirection(Transform Obj)
    {
        Vector3 dir = Obj.TransformVector(Direction);

        dir.y = 0;
        dir = dir.normalized * sqrm;

        return dir;
    }

    public void onclick()
    {
        //register finger id
        for (int i = 0; i < Input.touchCount; i++)
        {
            Vector3 localpos= rectTransform.InverseTransformPoint(Input.GetTouch(i).position);
            if (rectTransform.rect.Contains(localpos))
            {
                fingerID = Input.GetTouch(i).fingerId;
                startpos = Input.GetTouch(i).position;
                circle.position = startpos;
                swiping = true;
                Appear();
                return;
            }
        }

        if (Input.GetMouseButton(0))
        {
            swiping = true;
            Appear();
            circle.position = Input.mousePosition;
            startpos = Input.mousePosition;
        }
    }
    public void onrelease()
    {
        Disappear();
       // joystick.position = startpos;
        Direction = Vector2.zero;
        swiping = false;
        sqrm = 0;
    }

    void Appear()
    {
        circle.DOKill();
        joystick.DOKill();
        circle.GetComponent<Image>().DOFade(1f, 0.2f);
        joystick.DOScale(Vector3.one, 0.2f);
    }
    void Disappear()
    {
        circle.DOKill();
        joystick.DOKill();
        circle.GetComponent<Image>().DOFade(0f, 0.2f);
        joystick.DOScale(Vector3.zero, 0.2f);
    }
}
