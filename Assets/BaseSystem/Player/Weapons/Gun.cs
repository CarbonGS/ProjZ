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

    void Start()
    {
        if (gunTransform == null)
        {
            gunTransform = this.transform;
        }
        initialLocalPosition = gunTransform.localPosition;
    }

    void Update()
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

    public void ApplyRecoil()
    {
        StartCoroutine(RecoilRoutine());
    }

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
