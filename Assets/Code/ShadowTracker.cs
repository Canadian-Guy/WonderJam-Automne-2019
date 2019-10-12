using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowTracker : MonoBehaviour
{

    [HideInInspector] public int m_NumberOfShadowZones;
    private PlayerDeath m_PlayerDeathComponent;

    // Start is called before the first frame update
    void Start()
    {
        m_PlayerDeathComponent = GetComponent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterAZone()
    {
        m_NumberOfShadowZones++;
        if(m_NumberOfShadowZones > 0)
        {
            m_PlayerDeathComponent.m_isPraisingTheSun = false;
            Debug.Log("I am not praising the sun");
        }
    }

    public void ExitAZone()
    {
        m_NumberOfShadowZones--;
        if (m_NumberOfShadowZones <= 0)
        {
            m_PlayerDeathComponent.m_isPraisingTheSun = true;
            Debug.Log("I am now praising the sun");
        }
    }
}
