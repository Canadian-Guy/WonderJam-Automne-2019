using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class LeaderboardLoadMainMenu : MonoBehaviour
{
    void Update() {
        foreach(Rewired.Player player in ReInput.players.Players)
            if(player.GetButtonDown("Reset"))
                SceneManager.LoadScene("MainMenu");
    }
}
