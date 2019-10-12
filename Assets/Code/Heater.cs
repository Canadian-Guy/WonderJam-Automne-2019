using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer m_playerSpriteRenderer;
    [HideInInspector]
    public PlayerDeath m_playerDeath;
    public ParticleSystem m_smokeSystem;

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
            if(!m_smokeSystem.isEmitting)
                m_smokeSystem.Play();
            m_playerSpriteRenderer.color = new Vector4(1, (float)(1 - m_playerDeath.m_expositionRate), (float)(1 - m_playerDeath.m_expositionRate), m_playerSpriteRenderer.color.a);
            m_smokeSystem.emissionRate = (float)(100 * m_playerDeath.m_expositionRate);
        }
        else
        {
            if (m_smokeSystem.isEmitting)
                m_smokeSystem.Stop();
            m_playerSpriteRenderer.color = new Vector4(1, (float)(1 - m_playerDeath.m_expositionRate), (float)(1 - m_playerDeath.m_expositionRate), m_playerSpriteRenderer.color.a);
            m_smokeSystem.emissionRate = (float)(100 * m_playerDeath.m_expositionRate);
        }
    }
}
