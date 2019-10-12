using UnityEngine;

public class PlayerPressToPlay : MonoBehaviour
{
    [Tooltip("The object to show on screen when the player is not playing")]
    public GameObject m_objectToShow;

    [Tooltip("The id of the player to check for")]
    public int m_playerId;

    private Player m_player;
    private bool m_enabled = false;

    void Start() {
        m_player = Game.m_players.GetPlayerFromId(m_playerId);
        m_objectToShow.SetActive(false);
    }

    void Update() {
        if(m_player.m_hasEnteredGame && m_enabled) {
            m_objectToShow.SetActive(false);
            m_enabled = false;
        } else if(!m_player.m_hasEnteredGame && !m_enabled) {
            m_objectToShow.SetActive(true);
            m_enabled = true;
        }
    }
}
