using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Rewired;
using TMPro;

public class LeaderboardSpot : MonoBehaviour
{
    [Tooltip("The UI text displaying the spot's ranking")]
    public TMP_Text m_rankText;

    [Tooltip("The UI text displaying the spot's owner")]
    public TMP_Text m_nameText;

    [Tooltip("The UI text displaying the spot's deaths")]
    public TMP_Text m_deathText;

    [Tooltip("The UI text displaying the spot's score")]
    public TMP_Text m_timeText;

    [Tooltip("The UI images teaching the user to do things")]
    public List<Image> m_tutorialImages;

    [Tooltip("Can the name be modified?")]
    public bool m_editable;

    [HideInInspector] public LeaderboardScore m_score;
    [HideInInspector] public LeaderboardLoader m_loader;
    [HideInInspector] public bool m_editing;

    private int m_currentLetter = 65;
    private float m_lastScrollDown;

    void Update() {
        if(m_score != null && m_editing) {
            foreach(Rewired.Player player in ReInput.players.Players) {
                float yAxis = player.GetAxisRaw("MoveY");
                bool accepting = player.GetButtonDown("Jump");

                if(Mathf.Abs(yAxis) > 0 && Time.time - m_lastScrollDown >= 0.2f) {
                    m_lastScrollDown = Time.time;

                    if(yAxis > 0) m_currentLetter--;
                    if(yAxis < 0) m_currentLetter++;

                    if(m_currentLetter == 91) m_currentLetter = 65;
                    if(m_currentLetter == 64) m_currentLetter = 90;

                    m_nameText.text = m_nameText.text.Length <= 1 ? "" + (char) m_currentLetter : 
                                      m_nameText.text.Substring(0, m_nameText.text.Length - 1) + (char) m_currentLetter;
                }

                if(accepting) {
                    if(m_nameText.text.Length == 3) FinishEditing();
                    else {
                        m_nameText.text = m_nameText.text + "A";
                        m_currentLetter = 65;
                    }
                }
            }
        }
    }

    public void Set(int p_rank, LeaderboardScore p_score) {
        m_score = p_score;

        m_rankText.text = p_rank.ToString();
        m_nameText.text = p_score.Name;
        m_deathText.text = p_score.Deaths.ToString();
        m_timeText.text = Game.m_scores.ToDuration(p_score.Time);

        if(m_editable) {
            foreach(Image image in m_tutorialImages) image.enabled = true;

            m_editing = true;
        } else {
            foreach(Image image in m_tutorialImages) image.enabled = false;
        }
    }

    public void FinishEditing() {
        m_editing = false;
        m_score.Name = m_nameText.text;

        foreach(Image image in m_tutorialImages) image.enabled = false;

        if(m_score.Name.Length > 0)
            m_loader.AddScore(m_score);
    }
}
