using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBattery : Pickup
{
    public override void PickUp(Player player)
    {
        player.torchCounter += Amount;
        player.PickupText.text = "";
        player.PickupText.enabled = false;
        AudioSource.PlayOneShot(AudioClip);
        Destroy(gameObject);
    }
}
