using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRayBoxOld : MonoBehaviour
{
    public float Distance;
    public float Width;
    public bool Repel;

    private Vector3 Origin;

    //Front
    private RaycastHit Hit;
    private Ray RayMiddle;
    private Ray RayMiddleUp;
    private Ray RayMiddleDown;
    private Ray RayLeft;
    private Ray RayLeftUp;
    private Ray RayLeftDown;
    private Ray RayRight;
    private Ray RayRightUp;
    private Ray RayRightDown;
    private List<Ray> RayBoxFront = new List<Ray>();

    //Back
    private RaycastHit HitBack;
    private Ray RayMiddleBack;
    private Ray RayMiddleUpBack;
    private Ray RayMiddleDownBack;
    private Ray RayLeftBack;
    private Ray RayLeftUpBack;
    private Ray RayLeftDownBack;
    private Ray RayRightBack;
    private Ray RayRightUpBack;
    private Ray RayRightDownBack;
    private List<Ray> RayBoxBack = new List<Ray>();

    public void Update()
    {
        Origin = transform.position;
        Cast_RayBoxFront();
        Cast_RayBoxBack();
    }

    public void Cast_RayBoxFront()
    {
        Vector3 Direction = transform.forward;
        RayBoxFront = new List<Ray>();

        RayMiddle = new Ray(Origin, Direction * Distance + Direction);
        RayMiddleUp = new Ray(Origin + transform.up * Width, Direction * Distance + Direction);
        RayMiddleDown = new Ray(Origin - transform.up * Width, Direction * Distance + Direction);

        RayLeft = new Ray(Origin - transform.right * Width, Direction * Distance + Direction);
        RayLeftUp = new Ray(Origin - transform.right * Width + transform.up * Width, Direction * Distance + Direction);
        RayLeftDown = new Ray(Origin - transform.right * Width - transform.up * Width, Direction * Distance + Direction);

        RayRight = new Ray(Origin + transform.right * Width, Direction * Distance + Direction);
        RayRightUp = new Ray(Origin + transform.right * Width + transform.up * Width, Direction * Distance + Direction);
        RayRightDown = new Ray(Origin + transform.right * Width - transform.up * Width, Direction * Distance + Direction);

        RayBoxFront.Add(RayMiddle);
        RayBoxFront.Add(RayMiddleUp);
        RayBoxFront.Add(RayMiddleDown);

        RayBoxFront.Add(RayLeft);
        RayBoxFront.Add(RayLeftUp);
        RayBoxFront.Add(RayLeftDown);

        RayBoxFront.Add(RayRight);
        RayBoxFront.Add(RayRightUp);
        RayBoxFront.Add(RayRightDown);

        foreach (Ray Ray in RayBoxFront)
        {
            if (Physics.Raycast(Ray, out Hit, Distance))
            {
                Debug.DrawLine(Ray.origin, Hit.point, Color.green);

                if (Hit.collider.CompareTag("Monster"))
                {
                    if (Repel)
                    {
                        Hit.collider.gameObject.GetComponent<Monster>().Stop(transform);
                        break;
                    }
                    else
                    {
                        Hit.collider.gameObject.GetComponent<Monster>().Chase(transform);
                        break;
                    }
                }
            }
            else
            {
                Debug.DrawLine(Ray.origin, Ray.origin + Ray.direction * Distance, Color.green);
            }
        }
    }

    public void Cast_RayBoxBack()
    {
        Vector3 Direction = -transform.forward;
        RayBoxBack = new List<Ray>();

        RayMiddleBack = new Ray(Origin, Direction * Distance + Direction);
        RayMiddleUpBack = new Ray(Origin + transform.up * Width, Direction * Distance + Direction);
        RayMiddleDownBack = new Ray(Origin - transform.up * Width, Direction * Distance + Direction);

        RayLeftBack = new Ray(Origin - transform.right * Width, Direction * Distance + Direction);
        RayLeftUpBack = new Ray(Origin - transform.right * Width + transform.up * Width, Direction * Distance + Direction);
        RayLeftDownBack = new Ray(Origin - transform.right * Width - transform.up * Width, Direction * Distance + Direction);

        RayRightBack = new Ray(Origin + transform.right * Width, Direction * Distance + Direction);
        RayRightUpBack = new Ray(Origin + transform.right * Width + transform.up * Width, Direction * Distance + Direction);
        RayRightDownBack = new Ray(Origin + transform.right * Width - transform.up * Width, Direction * Distance + Direction);

        RayBoxBack.Add(RayMiddleBack);
        RayBoxBack.Add(RayMiddleUpBack);
        RayBoxBack.Add(RayMiddleDownBack);

        RayBoxBack.Add(RayLeftBack);
        RayBoxBack.Add(RayLeftUpBack);
        RayBoxBack.Add(RayLeftDownBack);

        RayBoxBack.Add(RayRightBack);
        RayBoxBack.Add(RayRightUpBack);
        RayBoxBack.Add(RayRightDownBack);

        foreach (Ray Ray in RayBoxBack)
        {
            if (Physics.Raycast(Ray, out Hit, Distance))
            {
                Debug.DrawLine(Ray.origin, Hit.point, Color.red);

                if (Hit.collider.CompareTag("Monster"))
                {
                    Hit.collider.gameObject.GetComponent<Monster>().Chase(transform);
                    break;
                }
            }
            else
            {
                Debug.DrawLine(Ray.origin, Ray.origin + Ray.direction * Distance, Color.red);
            }
        }
    }
}
