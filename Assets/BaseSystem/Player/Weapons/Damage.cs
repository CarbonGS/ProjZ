using UnityEngine;

public class Damage : MonoBehaviour
{
    public GameObject hitPrefab;

    public float damageVal;

    private Transform PlayerCamera;

    private void Start()
    {
        PlayerCamera = Camera.main.transform;
    }

    public void Shoot()
    {   
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.position, PlayerCamera.forward, out hit))
        {
            Entity entity = hit.transform.GetComponent<Entity>();
            if (entity != null)
            {
                entity.Health -= damageVal;
            }

            GameObject hole = Instantiate(hitPrefab, hit.point + hit.normal * 0.01f, Quaternion.LookRotation(-hit.normal));
            hole.transform.SetParent(hit.transform); // Makes the hole a child of the hit object
            hole.transform.localScale = Vector3.one * 0.1f; // Scale down the hole

            Collider col = hole.GetComponent<Collider>();
            if (col != null) col.enabled = false; // Disable collider if exists

            Destroy(hole, .15f); // Cleanup after 0.15 seconds
        }
    }
}
