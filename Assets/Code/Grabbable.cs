using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    public int weight = 1;
    public bool isCover;
    public Player EtienneIsDumb;

    protected override void Interact(Player player)
    {
        Grabber grabber = player.GetComponent<Grabber>();
        EtienneIsDumb = player;

        if (grabber.Grab(this))
            StopInteraction();

        if (isCover)
        {
            player.GetComponent<ShadowTracker>().EnterAZone();
        }
    }

    public void Release()
    {
        EtienneIsDumb.GetComponent<ShadowTracker>().ExitAZone();
        ResetInteraction();
    }
}
