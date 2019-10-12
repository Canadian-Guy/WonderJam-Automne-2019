using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{

    public BoxCollider2D m_BoxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        //m_BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {;
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShadowTracker>().EnterAZone();
            Debug.Log("Entered a shadow zone");
        }
        Debug.Log(collision.gameObject.name + " : " + gameObject.name + " : " + Time.time);

    }



    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ShadowTracker>().ExitAZone();
            Debug.Log("Exited a shadow zone");
        }
    }
}
