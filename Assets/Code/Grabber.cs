using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public GameObject inHandAnchor;
    public GameObject dropAnchor;
    public List<LayerMask> invalidGrabLayers;
    public List<LayerMask> invalidDropLayers;

    public bool Grabbing { get; set; }
    public Grabbable Grabbable { get; set; }

    private Player player;

    public void Awake()
    {

        player = gameObject.GetComponent<Player>();

        if (!inHandAnchor)
            Debug.Log("No anchor set for grabbed object");
    }

    public void Update()
    {
        if (Grabbing && Time.timeScale == 1f && player.m_rewiredPlayer.GetButtonDown("Interact"))
            Drop();
    }


    public bool Grab(Grabbable grabbable)
    {
        if (!Grabbing)
        {
            Grabbable = grabbable;
            SpriteRenderer renderer = grabbable.GetComponentInParent<SpriteRenderer>();
            bool overlap = Physics2D.OverlapBox(inHandAnchor.transform.position, new Vector2(renderer.bounds.size.x, renderer.bounds.size.y), 
                0, getCombineLayerMask(invalidGrabLayers));

            Debug.Log(invalidDropLayers.Count);

            if (!overlap)
            {
                Debug.Log("No overlap"); // Maybe play a sound here indicating an invalid position for grabbing the object
                grabbable.transform.parent.parent = inHandAnchor.transform;
                grabbable.transform.parent.localPosition = Vector2.zero;

                Grabbing = true;
                return true;
            }

            Debug.Log("Theres an overlap");
        }

        return false;
    }

    public void Drop()
    {
        SpriteRenderer renderer = Grabbable.GetComponentInParent<SpriteRenderer>();
        bool overlap = Physics2D.OverlapBox(dropAnchor.transform.position, new Vector2(renderer.bounds.size.x, renderer.bounds.size.y),
            0, getCombineLayerMask(invalidDropLayers));

        if (!overlap && player.GetComponent<CharController>().m_isGrounded)
        {

            Grabbable.transform.parent.parent = dropAnchor.transform;
            Grabbable.transform.parent.localPosition = Vector2.zero;
            Grabbable.transform.parent.parent = null;

            Grabbing = false;
            Grabbable.Release();
        }
    }

    public void ForceDrop()
    {
        if (Grabbing)
        {
            SpriteRenderer renderer = Grabbable.GetComponentInParent<SpriteRenderer>();
            Grabbable.transform.parent.parent = dropAnchor.transform;
            Grabbable.transform.parent.localPosition = new Vector2(-dropAnchor.transform.position.x/2, 0);
            Grabbable.transform.parent.parent = null;
            Grabbing = false;
            Grabbable.Release();
        }
    }


    private LayerMask getCombineLayerMask(List<LayerMask> layers)
    {
        LayerMask result = 0;

        foreach(LayerMask l in layers)
            result |= l;

        return result;
    }
}

