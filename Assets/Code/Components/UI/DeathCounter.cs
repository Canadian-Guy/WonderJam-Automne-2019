using UnityEngine;
using TMPro;

public class DeathCounter : MonoBehaviour
{
    public TMP_Text m_counter;

    void Update() {
        m_counter.text = Game.m_scores.m_deaths.ToString();
    }
}
