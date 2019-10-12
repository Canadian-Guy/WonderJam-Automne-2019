using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class GameStarter : MonoBehaviour
{
    void Update() {
        foreach(Rewired.Player player in ReInput.players.Players)
            if(player.GetButtonDown("Interact")) {
                for(int i = 0; i < 2; i++) {
                    DoubleBool interactions = new DoubleBool();

                    interactions.First = player.id == i;
                    interactions.Second = false;

                    Game.m_players.m_previousPlayerInteractions.Add(interactions);
                }

                StartGame();
            }
    }

    private void StartGame() {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
