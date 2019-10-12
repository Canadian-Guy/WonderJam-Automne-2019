using UnityEngine;
using Rewired;

public class Player : MonoBehaviour
{
    [Tooltip("The id assigned to this player, corresponds to the Rewired player id (starts at 0)")]
    public int m_playerId = 0;

    [HideInInspector] public CharController m_playerController;
    [HideInInspector] public Rewired.Player m_rewiredPlayer;

    private bool m_switched = false;

    public void Start() {
        Game.m_players.m_playerList.Add(this);

        m_rewiredPlayer = ReInput.players.GetPlayer(m_playerId);
        m_playerController = GetComponent<CharController>();
        m_playerController.m_player = this;
    }

    void Update() {
        if(Game.m_players.m_playingPlayers == 1 && m_rewiredPlayer.GetButtonDown("Switch")) {
            Player otherPlayer = Game.m_players.GetPlayerFromId(m_playerId == 0 ? 1 : 0);
            Controller lastActive = GetLastActiveController();

            otherPlayer.m_rewiredPlayer.controllers.AddController(lastActive, true);
            m_switched = !m_switched;
        } else if(Game.m_players.m_playingPlayers == 2 && m_switched) {
            Player otherPlayer = Game.m_players.GetPlayerFromId(m_playerId == 0 ? 1 : 0);
            Controller current = GetLastActiveController();
            Controller otherCurrent = otherPlayer.GetLastActiveController();

            otherPlayer.m_rewiredPlayer.controllers.AddController(current, true);
            m_rewiredPlayer.controllers.AddController(otherCurrent, true);
            m_switched = false;
        }
    }

    private Controller GetLastActiveController() {
        Controller controller = null;

        if(m_rewiredPlayer.controllers.joystickCount > 1)
            controller = m_rewiredPlayer.controllers.GetLastActiveController();
        else if(m_rewiredPlayer.controllers.joystickCount == 1)
            controller = m_rewiredPlayer.controllers.Joysticks[0];

        return controller;
    }
}
