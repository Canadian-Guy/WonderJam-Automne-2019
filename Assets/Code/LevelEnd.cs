using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public List<Collider2D> collisions = new List<Collider2D>();


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
            /*foreach(Collider2D collision2D in collisions)
            {
                collision2D.transform.position = new Vector3(0, 0, 0);
            }*/
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        //m_endPointCollider.OverlapCollider(m_contactFilter, collisions);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            m_playerWhoFinished++;
            collisions.Add(collision);
            if (m_playerWhoFinished ==  m_numberOfPlayer)
            {
                m_levelFinished = true;
            }
        }     
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            m_playerWhoFinished--;
            collisions.Remove(collision);
        }
    }

   
}
