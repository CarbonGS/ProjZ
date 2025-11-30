using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Gun : MonoBehaviour
{
    // Events for shooting
    public UnityEvent OnGunShoot;
    private bool isShooting = false;
    public float clickDuration = 0.2f; // Duration to consider as a click
    private float shootTimer = 0f;

    // Audio events
    public AudioSource shootSource;
    public AudioClip shootLoopClip;
    public AudioClip shootStopClip;

    // Fire rate
    [SerializeField] public float fireRate = 0.5f;
    private float nextFireTime = 0f;

    // Recoil parameters
    private Vector3 initialLocalPosition;
    public Transform gunTransform;
    public float recoilDistance = 0.1f;
    public float recoilSpeed = 10f;

    private bool isPaused = false; // Local pause state
    private Coroutine recoilCoroutine;

    /// <summary>
    /// Initializes the gun's position at the start of the game.
    /// </summary>
    void Start()
    {
        if (gunTransform == null)
        {
            gunTransform = this.transform;
        }
        initialLocalPosition = gunTransform.localPosition;
    }

    public void SetPauseState(bool paused) // Only called by PauseMenuManager
    {
        isPaused = paused;
    }

    /// <summary>
    /// Handles the shooting logic and recoil application in every frame.
    /// </summary>
    void Update()
    {
        if (!isPaused) // Don't shoot if the game is paused
        {

            if (Input.GetMouseButtonDown(0))
            {
                StartShooting();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopShooting();
            }

            if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
            {
                OnGunShoot.Invoke();
                ApplyRecoil();
                nextFireTime = Time.time + fireRate;
            }

            // Handle short click playback
            if (isShooting && !Input.GetMouseButton(0))
            {
                shootTimer += Time.deltaTime;
                if (shootTimer >= clickDuration)
                {
                    StopShooting();
                }
            }
        }
    }

    /// <summary>
    /// Starts the shooting audio and sets the shooting state to active.
    /// </summary>
    void StartShooting()
    {
        if (!isShooting)
        {
            shootSource.clip = shootLoopClip;
            shootSource.loop = true;

            shootSource.pitch = Random.Range(0.95f, 1.05f); // Randomize pitch slightly

            shootSource.Play();
            isShooting = true;
            shootTimer = 0f;
        }
    }

    /// <summary>
    /// Stops the shooting audio and sets the shooting state to inactive.
    /// </summary>
    void StopShooting()
    {
        if (isShooting)
        {
            shootSource.Stop();
            shootSource.loop = false;

            if (shootStopClip != null)
            {
                shootSource.PlayOneShot(shootStopClip);
            }

            isShooting = false;
        }
    }

    /// <summary>
    /// Applies recoil to the gun, simulating a realistic shooting effect.
    /// </summary>
    public void ApplyRecoil()
    {
        if (recoilCoroutine != null)
            StopCoroutine(recoilCoroutine);
        recoilCoroutine = StartCoroutine(RecoilRoutine());
    }

    /// <summary>
    /// Coroutine for handling the recoil animation.
    /// </summary>
    private IEnumerator RecoilRoutine()
    {
        Vector3 recoilPos = initialLocalPosition - Vector3.forward * recoilDistance;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;
            gunTransform.localPosition = Vector3.Lerp(initialLocalPosition, recoilPos, t);
            yield return null;
        }

        t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * recoilSpeed;
            gunTransform.localPosition = Vector3.Lerp(recoilPos, initialLocalPosition, t);
            yield return null;
        }
    }

}
