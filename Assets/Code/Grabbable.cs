using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    public int weight = 1;

    protected override void Interact(Player player)
    {
        Grabber grabber = player.GetComponent<Grabber>();

        if (grabber.Grab(this))
            StopInteraction();
    }

    public void Release()
    {
        ResetInteraction();
    }
}
