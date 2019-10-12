using UnityEngine;
using Rewired;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public static List<Player> m_players = new List<Player>();

    [Tooltip("The id assigned to this player, corresponds to the Rewired player id (starts at 0)")]
    public int m_playerId = 0;

    [HideInInspector] public CharController m_playerController;
    [HideInInspector] public Rewired.Player m_rewiredPlayer;

    public void Start() {
        m_players.Add(this);

        m_rewiredPlayer = ReInput.players.GetPlayer(m_playerId);
        m_playerController = GetComponent<CharController>();
        m_playerController.m_player = this;
    }

    public static Player GetPlayerFromId(int p_playerId) {
        foreach(Player player in m_players)
            if(player.m_playerId == p_playerId)
                return player;

        return null;
    }
}
