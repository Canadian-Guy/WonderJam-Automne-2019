using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Lockable
{
    public Key key;
    public GameObject objLock;
    public GameObject frame1;
    public SpriteRenderer frame1renderer;
    public GameObject frame2;
    public SpriteRenderer frame2renderer;
    public GameObject triggerZone;

    public new void Start()
    {
        base.Start();

        frame1renderer = frame1.GetComponent<SpriteRenderer>();
        frame2renderer = frame2.GetComponent<SpriteRenderer>();

        if (key)
            objLock.GetComponent<SpriteRenderer>().color = key.color;
        else if (!key)
            objLock.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Grabber grabber = collider.gameObject.GetComponent<Grabber>();

            if (grabber.Grabbing &&
                grabber.Grabbable.GetComponentInParent<Key>() == key && grabber.Grabbable.GetComponentInParent<Key>() != null)
            {
                Unlock();
                Destroy(key.gameObject);
                grabber.Grabbing = false;
                grabber.Grabbable = null;
            }
        }
    }

    protected override void Lock()
    {
        if (key)
            objLock.GetComponent<SpriteRenderer>().enabled = true;

        GetComponent<Collider2D>().enabled = true;
        triggerZone.GetComponent<BoxCollider2D>().enabled = true;
        frame1renderer.color = new Vector4(frame1renderer.color.r, frame1renderer.color.g, frame1renderer.color.b, 1);
        frame2renderer.color = new Vector4(frame2renderer.color.r, frame2renderer.color.g, frame2renderer.color.b, 1);
        locked = true;
    }

    protected override void Unlock()
    {
        if (key)
            objLock.GetComponent<SpriteRenderer>().enabled = false;

        GetComponent<Collider2D>().enabled = false;
        triggerZone.GetComponent<BoxCollider2D>().enabled = false;
        frame1renderer.color = new Vector4(frame1renderer.color.r, frame1renderer.color.g, frame1renderer.color.b, 0.1f);
        frame2renderer.color = new Vector4(frame2renderer.color.r, frame2renderer.color.g, frame2renderer.color.b, 0.1f);
        locked = false;
    }
}
