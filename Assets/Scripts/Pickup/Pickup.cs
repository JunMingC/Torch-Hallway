using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    public int Amount;
    [TextArea]    public string NearbyText;

    public abstract void PickUp(Player player);
}
