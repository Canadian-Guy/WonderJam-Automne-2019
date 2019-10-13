using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Lockable
{
    public Key key;
    public GameObject objLock;
    public GameObject frame;

    public new void Start()
    {
        base.Start();

        if (key)
            objLock.GetComponent<SpriteRenderer>().color = key.color;
    }

    public void OnCollisionEnter2D(Collision2D collider)
    {  
        if (collider.gameObject.tag == "Player")
        {
            Grabber grabber = collider.gameObject.GetComponent<Grabber>();

            if (grabber.Grabbing &&
                grabber.Grabbable.GetComponentInParent<Key>() == key)
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
        objLock.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        frame.GetComponent<SpriteRenderer>().color *= new Vector4(1, 1, 1, 2);
        gameObject.layer = 9;

        locked = true;
    }

    protected override void Unlock()
    {
        objLock.GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        frame.GetComponent<SpriteRenderer>().color *= new Vector4(1, 1, 1, .5f);
        gameObject.layer = 0;

        locked = false;
    }
}
