using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
    [Tooltip("Player that has the ability to reset de level")]
    public Player m_ResetPoweredPlayer;

    public GameEvent m_ResetEvent;

    // Update is called once per frame
    void Update()
    {
        if (m_ResetPoweredPlayer.m_rewiredPlayer.GetButtonDown("Interact"))
        {
            Reset();
        }
    }

    public void Reset()
    {
        m_ResetEvent.Raise();
    }
}
