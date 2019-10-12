using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer m_playerSpriteRenderer;
    [HideInInspector]
    public PlayerDeath m_playerDeath;

    // Start is called before the first frame update
    void Start()
    {
        m_playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_playerDeath = gameObject.GetComponent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerDeath.m_isPraisingTheSun)
        {
            m_playerSpriteRenderer.color = new Vector4(1, (float)(1 - m_playerDeath.m_expositionRate), (float)(1 - m_playerDeath.m_expositionRate), m_playerSpriteRenderer.color.a);
        }
        else
        {
            m_playerSpriteRenderer.color = new Vector4(1, (float)(1 - m_playerDeath.m_expositionRate), (float)(1 - m_playerDeath.m_expositionRate), m_playerSpriteRenderer.color.a);
        }
    }
}
