using System.Collections;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 0.00f;
    private float m_startTime;
    private TMP_Text m_text;

    void Start()
    {
        m_startTime = Time.time;
        m_text = GetComponent<TMP_Text>();
        StartCoroutine(SetTimer());
    }

    IEnumerator SetTimer()
    {
        time = Time.time - m_startTime;
        m_text.text = Game.m_scores.ToDuration(time);
        Game.m_scores.m_time = time;

        yield return new WaitForSeconds(0.05f);
        StartCoroutine(SetTimer());
    }
}
