using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class ScrollbarScroller : MonoBehaviour
{
    [Tooltip("The scrollbar to scroll with the controller")]
    public Scrollbar m_targetScrollbar;

    [Tooltip("How much of the scrollbar are we scrolling each step")]
    [Range(0, 0.1f)] public float m_step = 0.05f;

    void Update() {
        foreach(Rewired.Player player in ReInput.players.Players) {
            float yAxis = player.GetAxisRaw("MoveY");

            if(yAxis > 0) Increment();
            if(yAxis < 0) Decrement();
        }
    }

    private void Increment() {
        m_targetScrollbar.value = Mathf.Clamp(m_targetScrollbar.value + m_step, 0, 1);
    }

    private void Decrement() {
        m_targetScrollbar.value = Mathf.Clamp(m_targetScrollbar.value - m_step, 0, 1);
    }
}
