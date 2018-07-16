using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchRayBox : MonoBehaviour
{
    public GameObject Touch;
    public float Distance;
    public float Width;
    public bool Repel;

    private Vector3 Origin;

    //FrontRepel
    private RaycastHit HitRepel;
    private Ray RayMiddleRepel;
    private Ray RayMiddleUpRepel;
    private Ray RayMiddleDownRepel;
    private Ray RayLeftRepel;
    private Ray RayLeftUpRepel;
    private Ray RayLeftDownRepel;
    private Ray RayRightRepel;
    private Ray RayRightUpRepel;
    private Ray RayRightDownRepel;
    private List<Ray> RayBoxFrontRepel = new List<Ray>();

    //Front
    private RaycastHit Hit;
    private Ray RayMiddle;
    private Ray RayLeft;
    private Ray RayRight;
    private List<Ray> RayBoxFront = new List<Ray>();

    //Back
    private RaycastHit HitBack;
    private Ray RayMiddleBack;
    private Ray RayLeftBack;
    private Ray RayRightBack;
    private List<Ray> RayBoxBack = new List<Ray>();

    //Left
    private RaycastHit HitLeft;
    private Ray RayMiddleLeft;
    private Ray RayLeftLeft;
    private Ray RayRightLeft;
    private List<Ray> RayBoxLeft = new List<Ray>();

    //Right
    private RaycastHit HitRight;
    private Ray RayMiddleRight;
    private Ray RayLeftRight;
    private Ray RayRightRight;
    private List<Ray> RayBoxRight = new List<Ray>();

    public void Update()
    {
        Origin = transform.position;

        if (!Cast_RayBoxFrontRepel())
        {
            Cast_RayBoxFront();
            Cast_RayBoxBack();
            Cast_RayBoxLeft();
            Cast_RayBoxRight();
        }

    }

    public bool Cast_RayBoxFrontRepel()
    {
        if (Repel)
        {
            Vector3 TouchOrigin = Touch.transform.position;
            Vector3 Direction = Touch.transform.forward;
            RayBoxFrontRepel = new List<Ray>();

            RayMiddleRepel = new Ray(TouchOrigin, Direction * Distance);
            RayMiddleUpRepel = new Ray(TouchOrigin + Touch.transform.up * 0.5f, Direction * Distance + Touch.transform.up * Width);
            RayMiddleDownRepel = new Ray(TouchOrigin - Touch.transform.up * 0.5f, Direction * Distance - Touch.transform.up * Width);

            RayLeftRepel = new Ray(TouchOrigin - Touch.transform.right * 0.5f, Direction * Distance - Touch.transform.right * Width);
            RayLeftUpRepel = new Ray(TouchOrigin - Touch.transform.right * 0.5f + Touch.transform.up * 0.5f, Direction * Distance - Touch.transform.right * Width + Touch.transform.up * Width);
            RayLeftDownRepel = new Ray(TouchOrigin - Touch.transform.right * 0.5f - Touch.transform.up * 0.5f, Direction * Distance - Touch.transform.right * Width - Touch.transform.up * Width);

            RayRightRepel = new Ray(TouchOrigin + Touch.transform.right * 0.5f, Direction * Distance + Touch.transform.right * Width);
            RayRightUpRepel = new Ray(TouchOrigin + Touch.transform.right * 0.5f + Touch.transform.up * 0.5f, Direction * Distance + Touch.transform.right * Width + Touch.transform.up * Width);
            RayRightDownRepel = new Ray(TouchOrigin + Touch.transform.right * 0.5f - Touch.transform.up * 0.5f, Direction * Distance + Touch.transform.right * Width - Touch.transform.up * Width);

            RayBoxFrontRepel.Add(RayMiddleRepel);
            RayBoxFrontRepel.Add(RayMiddleUpRepel);
            RayBoxFrontRepel.Add(RayMiddleDownRepel);

            RayBoxFrontRepel.Add(RayLeftRepel);
            RayBoxFrontRepel.Add(RayLeftUpRepel);
            RayBoxFrontRepel.Add(RayLeftDownRepel);

            RayBoxFrontRepel.Add(RayRightRepel);
            RayBoxFrontRepel.Add(RayRightUpRepel);
            RayBoxFrontRepel.Add(RayRightDownRepel);

            foreach (Ray Ray in RayBoxFrontRepel)
            {
                if (Physics.Raycast(Ray, out Hit, Distance))
                {
                    Debug.DrawLine(Ray.origin, Hit.point, Color.green);

                    if (Hit.collider.CompareTag("Monster"))
                    {
                        Hit.collider.gameObject.GetComponent<Monster>().Stop(transform);
                        return true;
                    }
                }
                else
                {
                    Debug.DrawLine(Ray.origin, Ray.origin + Ray.direction * Distance, Color.green);
                }
            }
            return false;
        }
        return false;
    }

    public void Cast_RayBoxFront()
    {
        Vector3 Direction = transform.forward;
        RayBoxFront = new List<Ray>();

        RayMiddle = new Ray(Origin, Direction * Distance);
        RayLeft = new Ray(Origin - transform.right * 0.5f, Direction * Distance - transform.right * Width);
        RayRight = new Ray(Origin + transform.right * 0.5f, Direction * Distance + transform.right * Width);

        RayBoxFront.Add(RayMiddle);
        RayBoxFront.Add(RayLeft);
        RayBoxFront.Add(RayRight);

        foreach (Ray Ray in RayBoxFront)
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

    public void Cast_RayBoxBack()
    {
        Vector3 Direction = -transform.forward;
        RayBoxBack = new List<Ray>();

        RayMiddleBack = new Ray(Origin, Direction * Distance);
        RayLeftBack = new Ray(Origin - transform.right * 0.5f, Direction * Distance - transform.right * Width);
        RayRightBack = new Ray(Origin + transform.right * 0.5f, Direction * Distance + transform.right * Width);

        RayBoxBack.Add(RayMiddleBack);
        RayBoxBack.Add(RayLeftBack);
        RayBoxBack.Add(RayRightBack);

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

    public void Cast_RayBoxLeft()
    {
        Vector3 Direction = -transform.right;
        RayBoxLeft = new List<Ray>();

        RayMiddleLeft = new Ray(Origin, Direction * Distance);
        RayLeftLeft = new Ray(Origin - transform.forward * 0.5f, Direction * Distance - transform.forward * Width);
        RayRightLeft = new Ray(Origin + transform.forward * 0.5f, Direction * Distance + transform.forward * Width);

        RayBoxLeft.Add(RayMiddleLeft);
        RayBoxLeft.Add(RayLeftLeft);
        RayBoxLeft.Add(RayRightLeft);

        foreach (Ray Ray in RayBoxLeft)
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

    public void Cast_RayBoxRight()
    {
        Vector3 Direction = transform.right;
        RayBoxRight = new List<Ray>();

        RayMiddleRight = new Ray(Origin, Direction * Distance);
        RayLeftRight = new Ray(Origin - transform.forward * 0.5f, Direction * Distance - transform.forward * Width);
        RayRightRight = new Ray(Origin + transform.forward * 0.5f, Direction * Distance + transform.forward * Width);

        RayBoxRight.Add(RayMiddleRight);
        RayBoxRight.Add(RayLeftRight);
        RayBoxRight.Add(RayRightRight);

        foreach (Ray Ray in RayBoxRight)
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
