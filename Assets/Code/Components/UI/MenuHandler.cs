using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class MenuHandler : MonoBehaviour
{
    [Tooltip("The object holding the pause screen, if not in the main menu")]
    public GameObject m_pauseScreen;

    void Update() {
        bool mainMenu = SceneManager.GetActiveScene().buildIndex == 1;

        foreach(Rewired.Player player in ReInput.players.Players) {
            bool interact = player.GetButtonDown("Interact");
            bool reset = player.GetButtonDown("Reset");
            bool jump = player.GetButtonDown("Jump");
            bool pause = player.GetButtonDown("Pause");
            bool pauseScreenOpened = m_pauseScreen && m_pauseScreen.activeSelf;

            if(jump && mainMenu) {
                SceneManager.LoadScene("Leaderboard");
                return;
            }

            if(!mainMenu && !pauseScreenOpened) {
                if(pause) Pause();
            } else if(!mainMenu && pause) Resume();

            if(interact && mainMenu) {
                for(int i = 0; i < 2; i++) {
                    DoubleBool interactions = new DoubleBool();

                    interactions.First = player.id == i;
                    interactions.Second = false;

                    Game.m_players.m_previousPlayerInteractions.Add(interactions);
                }

                StartGame();
            } else if(interact && !mainMenu && pauseScreenOpened) Resume();
            else if(reset && mainMenu) Quit();
            else if(reset && !mainMenu && pauseScreenOpened) MainMenu();
        }
    }

    private void StartGame() {
        Game.m_scores.m_scores.Clear();
        Game.m_scores.ResetLevelVariables();

        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void MainMenu() {
        Time.timeScale = 1f;

        foreach(Player player in Game.m_players.m_playerList)
            player.ResetController();

        Game.m_players.m_canSwitch = true;
        Game.m_players.m_playerList.Clear();
        Game.m_players.m_previousPlayerInteractions.Clear();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    private void Quit() {
        Application.Quit();
    }

    private void Pause() {
        Time.timeScale = 0f;
        m_pauseScreen.SetActive(true);
    }

    private void Resume() {
        Time.timeScale = 1f;
        m_pauseScreen.SetActive(false);
    }
}
