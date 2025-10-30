using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] groanClips;
    public AudioClip attackClip;

    public float groanIntervalMin = 5f;
    public float groanIntervalMax = 15f;

    private float nextGroanTime = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ScheduleNextGroan();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextGroanTime)
        {
            PlayGroan();
            ScheduleNextGroan();
        }
    }

    void ScheduleNextGroan()
    {
        float interval = Random.Range(groanIntervalMin, groanIntervalMax);
        nextGroanTime = Time.time + interval;
    }

    void PlayGroan()
    {
        if (groanClips.Length == 0) return;
        int index = Random.Range(0, groanClips.Length);
        audioSource.PlayOneShot(groanClips[index]);
    }

    public void PlayAttackSound()
    {
        if (attackClip != null)
        {
            audioSource.PlayOneShot(attackClip);
        }
    }
}
