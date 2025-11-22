using UnityEngine;
using System.Collections.Generic;

public class ZombieRoundManager : MonoBehaviour
{
    public Transform player;
    public List<ZombieSpawner> spawners = new List<ZombieSpawner>();
    public float checkInterval = 1.0f;

    private GameManager gameManager;

    /// <summary>
    /// Initialize the ZombieRoundManager and start checking spawners.
    /// </summary>
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        InvokeRepeating(nameof(CheckSpawners), 0f, checkInterval);
    }

    /// <summary>
    /// Check each spawner to see if it should activate and spawn a zombie.
    /// </summary>
    void CheckSpawners()
    {
        if (gameManager == null || player == null) return;

        int maxZombies = gameManager.GetMaxZombiesThisRound();

        foreach (ZombieSpawner spawner in spawners)
        {
            float distance = Vector3.Distance(spawner.transform.position, player.position);
            if (distance <= spawner.activationDistance && distance > spawner.closeDistance)
            {
                
                if (!spawner.hasSpawned)
                {
                    spawner.SpawnZombie();
                }
            }
        }
    }
}