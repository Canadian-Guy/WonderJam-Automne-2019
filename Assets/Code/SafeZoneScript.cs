using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZoneScript : MonoBehaviour
{

    public BoxCollider2D m_BoxCollider2D;
    public GameEvent m_EnterEvent;
    public GameEvent m_ExitEvent;

    // Start is called before the first frame update
    void Start()
    {
        m_BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_EnterEvent.Raise();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            m_ExitEvent.Raise();
        }
    }
}
