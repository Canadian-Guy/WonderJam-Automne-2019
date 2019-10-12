using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public List<Player> m_playerList = new List<Player>();
    [HideInInspector] public int m_playingPlayers = 0;

    void Start()
    {
        StartCoroutine(UpdatePlayingPlayersAmount());
    }

    private IEnumerator UpdatePlayingPlayersAmount() {
        m_playingPlayers = 0;

        foreach(Player player in m_playerList)
            if(player.m_rewiredPlayer.controllers.joystickCount > 0)
                m_playingPlayers++;

        yield return new WaitForSeconds(0.5f);

        StartCoroutine(UpdatePlayingPlayersAmount());
    }

    public Player GetPlayerFromId(int p_playerId) {
        foreach(Player player in m_playerList)
            if(player.m_playerId == p_playerId)
                return player;

        return null;
    }
}
