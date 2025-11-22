using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] groanClips;
    public AudioClip attackClip;

    public float groanIntervalMin = 5f;
    public float groanIntervalMax = 15f;

    private float nextGroanTime = 0f;

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        ScheduleNextGroan();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (Time.time >= nextGroanTime)
        {
            PlayGroan();
            ScheduleNextGroan();
        }
    }

    /// <summary>
    /// Schedules the next groan sound to be played at a random interval
    /// </summary>
    void ScheduleNextGroan()
    {
        float interval = Random.Range(groanIntervalMin, groanIntervalMax);
        nextGroanTime = Time.time + interval;
    }

    /// <summary>
    /// Plays a random groan sound from the available groan clips
    /// </summary>
    void PlayGroan()
    {
        if (groanClips.Length == 0) return;
        int index = Random.Range(0, groanClips.Length);
        audioSource.PlayOneShot(groanClips[index]);
    }

    /// <summary>
    /// Plays the attack sound effect
    /// </summary>
    public void PlayAttackSound()
    {
        if (attackClip != null)
        {
            audioSource.PlayOneShot(attackClip);
        }
    }
}
