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
    [HideInInspector] public SpriteRenderer m_spriteRenderer;
    [HideInInspector] public SpriteRenderer m_switchTooltip;
    [HideInInspector] public Animator m_animator;

    public void Start() {
        m_switched = false;
        m_hasEnteredGame = false;
        m_puppet = false;
        m_lastUsed = null;

        Game.m_players.m_playerList.Add(this);

        m_rewiredPlayer = ReInput.players.GetPlayer(m_playerId);
        m_playerController = GetComponent<CharController>();
        m_playerController.m_player = this;

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_switchTooltip = transform.Find("SwitchTooltip").GetComponent<SpriteRenderer>();
        m_switchTooltip.enabled = false;

        m_animator = GetComponent<Animator>();
        m_animator.SetBool("isWalking", false);

        m_audioSource = gameObject.AddComponent<AudioSource>();
        Game.m_audio.AddAudioSource(m_audioSource, AudioCategories.SFX);
    }

    void Update() {
        if(Time.timeScale == 0f) return;

        Player otherPlayer = Game.m_players.GetPlayerFromId(m_playerId == 0 ? 1 : 0);

        if(!m_hasEnteredGame && m_rewiredPlayer.GetButtonDown("Interact") &&
            (IsPlaying() || (m_puppet && otherPlayer.m_lastUsed != GetLastActiveController()))) {
            if(m_puppet)
                otherPlayer.m_rewiredPlayer.controllers.AddController(otherPlayer.m_lastUsed, true);

            m_hasEnteredGame = true;
            m_switched = false;
            m_puppet = false;
            otherPlayer.m_switched = false;

            if(otherPlayer.m_hasEnteredGame) Game.m_players.m_canSwitch = false;

            ToggleSwitchTooltip(false);
            otherPlayer.ToggleSwitchTooltip(false);
        }

        if(Game.m_players.m_canSwitch && Game.m_players.m_playingPlayers == 1 && 
            (m_hasEnteredGame || m_puppet && otherPlayer.m_lastUsed == GetLastActiveController()) && m_rewiredPlayer.GetButtonDown("Switch")) {
            if(m_hasEnteredGame) m_lastUsed = GetLastActiveController();
            otherPlayer.m_rewiredPlayer.controllers.AddController(GetLastActiveController(), true);

            m_switched = m_hasEnteredGame;
            otherPlayer.m_switched = otherPlayer.m_hasEnteredGame ? !otherPlayer.m_switched : false;
            m_puppet = m_hasEnteredGame ? false : otherPlayer.m_switched;
            otherPlayer.m_puppet = m_switched;

            if(otherPlayer.m_puppet) {
                ToggleSwitchTooltip(true);
                otherPlayer.ToggleSwitchTooltip(false);
            } else {
                ToggleSwitchTooltip(true);
                otherPlayer.ToggleSwitchTooltip(false);
            }
        }

        if(Game.m_players.m_playingPlayers == 1 && !m_hasEnteredGame && !m_switchTooltip.enabled && !otherPlayer.m_switchTooltip.enabled)
            ToggleSwitchTooltip(true);
    }

    public void ResetController() {
        if(m_switched && m_lastUsed != null)
            m_rewiredPlayer.controllers.AddController(m_lastUsed, true);
    }

    public bool IsPlaying() {
        Player other = Game.m_players.GetPlayerFromId(m_playerId == 0 ? 1 : 0);

        return (m_rewiredPlayer.controllers.GetLastActiveController() != null && !other.m_switched) ||
               (other.m_rewiredPlayer.controllers.GetLastActiveController() != null && m_switched);
    }

    public void ToggleSwitchTooltip(bool p_on) {
        m_switchTooltip.enabled = p_on;
        m_spriteRenderer.color = new Color(m_spriteRenderer.color.r,
                                           m_spriteRenderer.color.g,
                                           m_spriteRenderer.color.b,
                                           p_on ? 100f / 255f : 1f);
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
