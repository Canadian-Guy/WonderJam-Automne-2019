using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer m_playerSpriteRenderer;
    [HideInInspector]
    public PlayerDeath m_playerDeath;
    [Range(0,1)]public double m_flashSpeed = 0.1f;
    [HideInInspector]
    public double m_baseFlashSpeed;
    [HideInInspector]
    public double m_flashAcceleration;
    [HideInInspector]
    public double m_baseFlashAcceleration;
    [HideInInspector]
    public double m_tempFlashSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_playerDeath = gameObject.GetComponent<PlayerDeath>();
        m_baseFlashSpeed = m_flashSpeed;
        m_tempFlashSpeed = m_flashSpeed;
        m_flashAcceleration = 0.0001d;
        m_baseFlashAcceleration = m_flashAcceleration;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_playerDeath.m_isPraisingTheSun & 1 / m_flashSpeed <= 1)
        {
            m_playerSpriteRenderer.enabled = !m_playerSpriteRenderer.enabled;
            m_flashSpeed = m_baseFlashSpeed * m_flashAcceleration;
            m_flashAcceleration += m_flashAcceleration;
        }
        else if (m_playerDeath.m_isPraisingTheSun)
        {
            m_flashSpeed += m_flashSpeed;
        }
        else
        {
            //if (m_flashAcceleration > m_baseFlashAcceleration & m_flashSpeed > m_baseFlashSpeed)
            //{
            //    m_playerSpriteRenderer.enabled = !m_playerSpriteRenderer.enabled;
            //    m_flashSpeed = m_baseFlashSpeed * m_flashAcceleration;
            //    m_flashAcceleration -= m_flashAcceleration;
            //    m_flashSpeed -= m_flashSpeed;
            //}
            m_flashSpeed = m_baseFlashSpeed;
            m_flashAcceleration = m_baseFlashAcceleration;
            m_playerSpriteRenderer.enabled = true;
        }
    }
}
