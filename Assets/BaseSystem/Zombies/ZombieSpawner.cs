using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Prefab of the zombie to spawn
    public Transform player; // Reference to the player's transform
    public float activationDistance = 65.0f; // Distance to activate spawning
    public float closeDistance = 5.0f; // Distance to stop spawning
    public bool hasSpawned = false; // Flag to check if a zombie has been spawned
    public float spawnInterval; // Time interval between spawns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnZombie()
    {
        if (hasSpawned) return; // Prevent double-spawning

        // Check with GameManager if spawning is allowed
        GameManager gm = FindObjectOfType<GameManager>();
        if (!gm.CanSpawnZombie()) return;

        gm.RegisterSpawn();
        hasSpawned = true;

        // Instantiate the zombie prefab at the spawner's position
        GameObject zombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
        zombie.transform.localScale *= 2;

        // Add components
        CapsuleCollider col = zombie.AddComponent<CapsuleCollider>();
        col.radius = 0.2f;
        col.height = 1.2f;
        col.center = new Vector3(0, 0.4f, 0);

        Entity ent = zombie.AddComponent<Entity>();
        ent.StartHealth = 500;

        // Assign player to ZombieAI
        ZombieAI ai = zombie.GetComponent<ZombieAI>();
        if (ai != null)
        {
            ai.player = player;
        }

        // Adjust spawn interval based on game manager's spawn rate multiplier
        float spawnRateMultiplier = FindObjectOfType<GameManager>().spawnRateMultiplier;
        float cooldown = Mathf.Max(spawnInterval / spawnRateMultiplier, 3f); // Cap at minimum 3 seconds
        Invoke("ResetSpawn", cooldown);
    }

    void ResetSpawn()
    {
        hasSpawned = false;
    }
}
