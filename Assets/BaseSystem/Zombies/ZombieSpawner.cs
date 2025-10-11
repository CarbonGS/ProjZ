using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Prefab of the zombie to spawn
    public Transform player; // Reference to the player's transform
    public float activationDistance = 20.0f; // Distance to activate spawning
    public float closeDistance = 10.0f; // Distance to stop spawning
    public bool hasSpawned = false; // Flag to check if a zombie has been spawned
    public float spawnInterval = 5.0f; // Time interval between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpawned || player == null) return;
        float distance = Vector3.Distance(transform.position, player.position); // Calculate distance to player

        if (distance <= activationDistance && distance > closeDistance) // Check if player is within distance for zombie spawning
        {
            SpawnZombie();
            hasSpawned = true;
        }
    }

    void SpawnZombie()
    {
        GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        zombie.transform.localScale *= 2;

        // Add collider
        CapsuleCollider col = zombie.AddComponent<CapsuleCollider>();
        col.radius = 0.2f;
        col.height = 1.2f;
        col.center = new Vector3(0, 0.4f, 0); // Adjust center to align with the model

        // Add entity component
        Entity ent = zombie.AddComponent<Entity>();
        ent.StartHealth = 500; // Set health value

        // Add player reference to ZombieAI
        ZombieAI ai = zombie.GetComponent<ZombieAI>();
        if (ai != null)
        {
            ai.player = player;
        }


        Invoke("ResetSpawn", spawnInterval); // Reset spawn flag after interval
    }

    void ResetSpawn()
    {
        hasSpawned = false;
    }
}
