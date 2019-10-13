using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStopper : MonoBehaviour
{

    [Tooltip("Sound to play instead of the background")]
    public SimpleAudioEvent m_IntroClip;


    private AudioSource m_IntroSource;
    private AudioSource m_AmbiantNoiseSource;
    private AudioSource m_BackgroundMusicSource;

    private void Awake()
    {
        m_IntroSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Game.m_audio.GetComponents<AudioSource>()[0].Stop();
        Game.m_audio.GetComponents<AudioSource>()[1].Stop();

        m_IntroClip.Play(m_IntroSource);
    }
}
