using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Rewired;

public class OverviewLoader : MonoBehaviour
{
    public Transform m_content;
    public GameObject m_levelScorePrefab;

    void Start() {
        if(Game.m_scores.m_scores.Count == 0) return;

        foreach(LevelScore score in Game.m_scores.m_scores) {
            GameObject scoreObject = Instantiate(m_levelScorePrefab, m_content.transform);

            scoreObject.transform.Find("Level").GetComponent<TMP_Text>().text = score.m_levelId.ToString();
            scoreObject.transform.Find("Time").GetComponent<TMP_Text>().text = Game.m_scores.ToDuration(score.m_time);
            scoreObject.transform.Find("Deaths").GetComponent<TMP_Text>().text = score.m_deaths.ToString();
            scoreObject.transform.Find("Total").GetComponent<TMP_Text>().text = Game.m_scores.ToDuration(Game.m_scores.GetAdjustedTime(score));
        }
    }

    void Update() {
        foreach(Rewired.Player player in ReInput.players.Players) {
            if(player.GetButtonDown("Interact")) {
                SaveScoreToJSON();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
            }
        }
    }

    public void SaveScoreToJSON() {
        LeaderboardScore score = new LeaderboardScore("", Game.m_scores.GetTotalAdjustedTime(), Game.m_scores.GetTotalDeaths());
        WriteToJSON(score, Application.dataPath + "/Data/PendingScores.JSON");
    }

    private void WriteToJSON(LeaderboardScore p_score, string p_fileName) {
        System.IO.StreamWriter Writer = new System.IO.StreamWriter(p_fileName, false);

        Writer.Write(JsonUtility.ToJson(p_score));
        Writer.Close();
    }
}
