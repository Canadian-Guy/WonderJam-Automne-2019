using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public SimpleAudioEvent m_AmbiantNoises;
    public SimpleAudioEvent m_BackgroundMusic;

    private AudioSource m_AmbiantSource;
    private AudioSource m_BackgroundMusicSource;

    private void Awake()
    {
        m_AmbiantSource = GetComponents<AudioSource>()[0];
        m_BackgroundMusicSource = GetComponents<AudioSource>()[1];

    }

    // Start is called before the first frame update
    void Start()
    {
        StartTheShit();
    }
    
    public void StartTheShit()
    {
        m_AmbiantNoises.Play(m_AmbiantSource);
        m_BackgroundMusic.Play(m_BackgroundMusicSource);
    }

}
