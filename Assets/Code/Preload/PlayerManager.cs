using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public List<Player> m_playerList = new List<Player>();
    [HideInInspector] public int m_playingPlayers = 0;
    [HideInInspector] public List<bool> m_previousPlayerInteractions;

    void Start() {
        m_previousPlayerInteractions = new List<bool>();
        SceneManager.sceneLoaded += OnSceneLoad;
        StartCoroutine(UpdatePlayingPlayersAmount());
    }

    private void OnSceneLoad(Scene p_scene, LoadSceneMode p_mode) {
        if(m_playerList.Count > 0) {
            m_previousPlayerInteractions.Clear();
            
            foreach(Player player in m_playerList) 
                m_previousPlayerInteractions.Add(player.m_hasEnteredGame && player.IsPlaying());
        }

        m_playerList.Clear();
        m_playingPlayers = 0;
    }

    private IEnumerator UpdatePlayingPlayersAmount() {
        m_playingPlayers = 0;

        if(m_previousPlayerInteractions.Count > 0 && m_playerList.Count > 0) {
            for(int i = 0; i < m_playerList.Count; i++)
                m_playerList.Find(p => p.m_playerId == i).m_hasEnteredGame = m_previousPlayerInteractions[i];

            m_previousPlayerInteractions.Clear();
        }

        foreach(Player player in m_playerList)
            if(player.IsPlaying()) m_playingPlayers++;

        yield return new WaitForSeconds(0.25f);

        StartCoroutine(UpdatePlayingPlayersAmount());
    }

    public Player GetPlayerFromId(int p_playerId) {
        foreach(Player player in m_playerList)
            if(player.m_playerId == p_playerId)
                return player;

        return null;
    }
}
