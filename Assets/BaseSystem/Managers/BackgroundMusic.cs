using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip backgroundMusicClip;

    /// <summary>
    /// Initializes the audio source by assigning the background music clip, 
    /// enabling looping, and starting playback.
    /// </summary>
    void Start()
    {
        audioSource.clip = backgroundMusicClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// Ensures the background music keeps playing by restarting it if it stops.
    /// </summary>
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
