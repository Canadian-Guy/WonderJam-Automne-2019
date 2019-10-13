using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public List<LevelScore> m_scores;
    public float m_time;
    public int m_deaths;

    void Start() {
        m_scores = new List<LevelScore>();
        ResetLevelVariables();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public int GetTotalDeaths() {
        int deaths = 0;

        foreach(LevelScore score in m_scores)
            deaths += score.m_deaths;

        return deaths;
    }

    public float GetTotalBaseTime() {
        float time = 0;

        foreach(LevelScore score in m_scores)
            time += score.m_time;

        return time;
    }

    public float GetAdjustedTime(LevelScore p_score) {
        return p_score.m_time + p_score.m_deaths * 15f;
    }

    public float GetTotalAdjustedTime() {
        return GetTotalBaseTime() + GetTotalDeaths() * 15f;
    }

    private void OnSceneLoad(Scene p_scene, LoadSceneMode p_mode) {
        if(m_time > 0) {
            LevelScore score = new LevelScore();

            score.m_levelId = m_scores.Count + 1;
            score.m_time = m_time;
            score.m_deaths = m_deaths;

            m_scores.Add(score);
        }

        ResetLevelVariables();
    }

    public void ResetLevelVariables() {
        m_time = 0;
        m_deaths = 0;
    }

    public string ToDuration(float p_time) {
        string display = "";
        float leftover = p_time;
        int hours = Mathf.FloorToInt(leftover / 3600);
        leftover -= hours * 3600;

        int minutes = Mathf.FloorToInt(leftover / 60);
        leftover -= minutes * 60;

        int seconds = Mathf.FloorToInt(leftover);

        if(hours > 0) display += hours + "h";
        if(minutes > 0) display += minutes + "m";
        if(seconds > 0) display += seconds + "s";

        return display;
    }
}

[System.Serializable]
public class LevelScore {
    public int m_levelId;
    public float m_time;
    public int m_deaths;
}