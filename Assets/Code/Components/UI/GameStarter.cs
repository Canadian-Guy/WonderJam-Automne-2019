using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class GameStarter : MonoBehaviour
{
    void Update() {
        foreach(Rewired.Player player in ReInput.players.Players)
            if(player.GetButtonDown("Interact")) {
                for(int i = 0; i < 2; i++)
                    Game.m_players.m_previousPlayerInteractions.Add(player.id == i);

                StartGame();
            }
    }

    private void StartGame() {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
