using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flasher : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer m_playerSpriteRenderer;
    [HideInInspector]
    public PlayerDeath m_playerDeath;
    [Range(0,1)]public float m_flashSpeed;
    // Start is called before the first frame update
    void Start()
    {
        m_playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_playerDeath = gameObject.GetComponent<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
