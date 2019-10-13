using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float time = 0.00f;
    private float m_startTime;

    void Start()
    {
        m_startTime = Time.time;
        StartCoroutine(SetTimer());
    }

    IEnumerator SetTimer()
    {
        gameObject.GetComponent<TMP_Text>().text = time.ToString("F2") + " s";
        time = Time.time - m_startTime;
        Game.m_scores.m_time = time;
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(SetTimer());
    }
}
