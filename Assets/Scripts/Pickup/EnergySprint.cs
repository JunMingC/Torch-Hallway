using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergySprint : Pickup
{
    public override void PickUp(Player player)
    {
        player.powerMode = true;
        player.powerCounter = Time.time + 5f;
        AudioSource.PlayOneShot(AudioClip);
    }
}
