using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    //[HideInInspector]
    //CircleCollider2D m_endPointCollider;
    //public ContactFilter2D m_contactFilter;
    [HideInInspector]
    public int m_playerWhoFinished;
    public int m_numberOfPlayer = 2;
    [HideInInspector]
    public bool m_levelFinished = false;
    [HideInInspector]
    public List<Collision2D> collisions = new List<Collision2D>();

    // Start is called before the first frame update
    void Start()
    {
        //m_endPointCollider = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_levelFinished)
        {
            foreach(Collision2D collision2D in collisions)
            {
                collision2D.transform.position = new Vector3(0, 0, 0);
            }
        }
        //m_endPointCollider.OverlapCollider(m_contactFilter, collisions);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            m_playerWhoFinished++;
            collisions.Add(collision);
            if (m_playerWhoFinished ==  m_numberOfPlayer)
            {
                m_levelFinished = true;
            }
        }     
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            m_playerWhoFinished--;
            collisions.Remove(collision);
        }
    }
}
