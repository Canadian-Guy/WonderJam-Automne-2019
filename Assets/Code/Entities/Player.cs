using UnityEngine;
using Rewired;

public class Player : MonoBehaviour
{
    [Tooltip("The id assigned to this player, corresponds to the Rewired player id (starts at 0)")]
    public int m_playerId = 0;

    [HideInInspector] public CharController m_playerController;
    [HideInInspector] public Rewired.Player m_rewiredPlayer;
    [HideInInspector] public bool m_switched = false;
    [HideInInspector] public bool m_hasEnteredGame = false;
    [HideInInspector] public bool m_puppet = false;
    [HideInInspector] public Controller m_lastUsed;
    [HideInInspector] public AudioSource m_audioSource;

    public void Start() {
        Game.m_players.m_playerList.Add(this);

        m_rewiredPlayer = ReInput.players.GetPlayer(m_playerId);
        m_playerController = GetComponent<CharController>();
        m_playerController.m_player = this;

        m_audioSource = gameObject.AddComponent<AudioSource>();
        Game.m_audio.AddAudioSource(m_audioSource, AudioCategories.SFX);
    }

    void Update() {
        Player otherPlayer = Game.m_players.GetPlayerFromId(m_playerId == 0 ? 1 : 0);

        if(!m_hasEnteredGame && m_rewiredPlayer.GetButtonDown("Interact") &&
            (IsPlaying() || (m_puppet && otherPlayer.m_lastUsed != GetLastActiveController()))) {
            if(m_puppet)
                otherPlayer.m_rewiredPlayer.controllers.AddController(otherPlayer.m_lastUsed, true);

            m_hasEnteredGame = true;
            m_switched = false;
            m_puppet = false;
            otherPlayer.m_switched = false;
        }

        if(Game.m_players.m_playingPlayers == 1 && (m_hasEnteredGame || m_puppet && otherPlayer.m_lastUsed == GetLastActiveController()) && 
            m_rewiredPlayer.GetButtonDown("Switch")) {
            if(m_hasEnteredGame) m_lastUsed = GetLastActiveController();
            otherPlayer.m_rewiredPlayer.controllers.AddController(GetLastActiveController(), true);

            m_switched = m_hasEnteredGame;
            otherPlayer.m_switched = otherPlayer.m_hasEnteredGame ? !otherPlayer.m_switched : false;
            m_puppet = m_hasEnteredGame ? false : otherPlayer.m_switched;
            otherPlayer.m_puppet = m_switched;
        }
    }

    public bool IsPlaying() {
        Player other = Game.m_players.GetPlayerFromId(m_playerId == 0 ? 1 : 0);

        return (m_rewiredPlayer.controllers.GetLastActiveController() != null && !other.m_switched) ||
               (other.m_rewiredPlayer.controllers.GetLastActiveController() != null && m_switched);
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
