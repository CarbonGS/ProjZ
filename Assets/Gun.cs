using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Gun : MonoBehaviour
{
    // Events for shooting and reloading
    public UnityEvent OnGunShoot;
    public UnityEvent OnGunReload;

    // Fire rate
    public float fireRate = 0.5f;
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
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            OnGunShoot.Invoke();
            ApplyRecoil();
            nextFireTime = Time.time + fireRate;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnGunReload.Invoke();
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
