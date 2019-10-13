using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;

public class TutorielManager : MonoBehaviour
{
    [Tooltip("Put tutorial panels here, in order. Must put the intro text first.")]
    public List<GameObject> m_TutorialSlides;

    [Tooltip("AudioSource for the intro")]
    public AudioSource m_IntroSource;
    // Start is called before the first frame update
    void Start()
    {
        if (Game.m_audio.GetComponents<AudioSource>()[0].isPlaying)
            Game.m_audio.GetComponents<AudioSource>()[0].Stop();
        if (Game.m_audio.GetComponents<AudioSource>()[1].isPlaying)
            Game.m_audio.GetComponents<AudioSource>()[1].Stop();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Rewired.Player player in ReInput.players.Players)
        {
            bool interact = player.GetButtonDown("Interact");

            if (interact)
            {
                if (!Game.m_audio.GetComponents<AudioSource>()[0].isPlaying)
                    Game.m_audio.GetComponent<BackgroundMusicManager>().StartTheShit();

                if (m_IntroSource.isPlaying)
                    m_IntroSource.Stop();

                NextSlide();
            }

        }
    }

    private void NextSlide()
    {
        m_TutorialSlides[0].SetActive(false);
        if (m_TutorialSlides.Count > 1)
            m_TutorialSlides[1].SetActive(true);
        else
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }

        m_TutorialSlides.Remove(m_TutorialSlides[0]);

        Debug.Log("NEXT SLIDEEEEEEEEE");
    }

}


