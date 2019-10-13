using UnityEngine;
using Rewired;

public class LevelReset : MonoBehaviour
{
    [Tooltip("Event to raise when we need a reset")]
    public GameEvent m_ResetEvent;

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0f) return;

        foreach(Rewired.Player player in ReInput.players.Players)
            if(player.GetButtonDown("Reset")) Reset();
    }

    public void Reset()
    {
        m_ResetEvent.Raise();
    }
}
