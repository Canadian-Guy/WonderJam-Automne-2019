using System;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardLoader : MonoBehaviour
{
    [Tooltip("The leaderboard spot's prefab")]
    public GameObject m_spotPrefab;

    [Tooltip("The amount of scores to load in the leaderboard (maximum)")]
    public int m_loadedAmount;

    private List<GameObject> m_instantiatedObjects;

    void Start() {
        Load();
    }

    public void Load() {
        if(m_instantiatedObjects != null && m_instantiatedObjects.Count > 0) return;

        m_instantiatedObjects = new List<GameObject>();

        List<LeaderboardScore> scores = GetLocalScores();
        LeaderboardScore pending = GetPendingScore();

        if(pending != null) scores.Add(pending);

        if(scores.Count > 0)
            scores.Sort(CompareScores);

        for(int i = 1; i <= m_loadedAmount; i++) {
            if(scores.Count < i) break;

            GameObject spotObj;
            bool defaultName = false;
            LeaderboardScore score = scores[i - 1];

            if(score.Name.Length == 0 && score != pending) defaultName = true;

            spotObj = Instantiate(m_spotPrefab, transform.Find("Viewport").Find("Content"));

            m_instantiatedObjects.Add(spotObj);
            LeaderboardSpot spot = spotObj.GetComponent<LeaderboardSpot>();

            if(defaultName) score.Name = "AAA";
            if(score == pending) spot.m_editable = true;

            spot.m_loader = this;
            spot.Set(i, score);
        }
    }

    public List<LeaderboardScore> GetLocalScores() {
        List<LeaderboardScore> scores = new List<LeaderboardScore>();
        System.IO.StreamReader Reader = new System.IO.StreamReader(Application.dataPath + "/Data/LocalLeaderboard.JSON");

        for(string json = Reader.ReadLine(); json != null; json = Reader.ReadLine()) {
            scores.Add(JsonUtility.FromJson<LeaderboardScore>(json));
        }

        Reader.Close();

        return scores;
    }

    public void AddScore(LeaderboardScore p_score) {
        List<LeaderboardScore> offline = GetLocalScores();

        offline.Sort(CompareScores);

        System.IO.StreamWriter Local = new System.IO.StreamWriter(Application.dataPath + "/Data/LocalLeaderboard.JSON", true);

        if(offline.Count < m_loadedAmount || offline[m_loadedAmount - 1].Time < p_score.Time) // if we make it in top x
            Local.Write((offline.Count > 0 ? "\n" : "") + JsonUtility.ToJson(p_score));

        Local.Close();

        DeletePendingScore();
    }

    public LeaderboardScore GetPendingScore() {
        System.IO.StreamReader Reader = new System.IO.StreamReader(Application.dataPath + "/Data/PendingScores.JSON");
        LeaderboardScore score = JsonUtility.FromJson<LeaderboardScore>(Reader.ReadLine());

        Reader.Close();
        return score;
    }

    public void DeletePendingScore() {
        System.IO.StreamWriter Writer = new System.IO.StreamWriter(Application.dataPath + "/Data/PendingScores.JSON", false);

        Writer.Write("");
        Writer.Close();
    }

    public static int CompareScores(LeaderboardScore first, LeaderboardScore second) {
        try {
            return second.Time - first.Time > 0 ? -1 : 1;
        } catch(NullReferenceException) {
            if(first == null) return -1;
            else return 1;
        }
    }
}

[Serializable]
public class LeaderboardScore {
    public string Name;
    public float Time;
    public int Deaths;

    public LeaderboardScore(string p_name, float p_time, int p_deaths) {
        Name = p_name;
        Time = p_time;
        Deaths = p_deaths;
    }
}