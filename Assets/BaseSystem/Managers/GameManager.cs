using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public int currentRound = 1;
    public int zombieKills = 0;
    public int killsToNextRound = 10;
    public int zombiesSpawned = 0;
    public int activeZombies = 0;

    public float healthMultiplier = 1f;
    public float damageMultiplier = 1f;
    public float spawnRateMultiplier = 0.5f;

    /// <summary>
    /// Determines if a new zombie can be spawned based on the current game state.
    /// </summary>
    /// <returns>True if a zombie can be spawned, false otherwise.</returns>
    public bool CanSpawnZombie()
    {
        return zombiesSpawned < killsToNextRound && activeZombies < killsToNextRound;
    }

    /// <summary>
    /// Registers the spawn of a new zombie, incrementing the relevant counters.
    /// </summary>
    public void RegisterSpawn()
    {
        zombiesSpawned++;
        activeZombies++;
    }

    /// <summary>
    /// Registers the kill of a zombie, decrementing the active zombie count and incrementing the kill count.
    /// Advances to the next round if the required number of kills has been reached.
    /// </summary>
    public void RegisterKill()
    {
        activeZombies--;
        zombieKills++;
        Debug.Log($"Zombie killed! Total kills this round: {zombieKills}/{killsToNextRound}");
        if (zombieKills >= killsToNextRound)
        {
            AdvanceRound();
        }
    }

    /// <summary>
    /// Unity's awake method, called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // Initialization logic here
    }

    /// <summary>
    /// Unity's start method, called before the first frame update.
    /// Used here to log the initial state of the GameManager.
    /// </summary>
    void Start()
    {
        Debug.Log($"GameManager initialized. spawnRateMultiplier = {spawnRateMultiplier}");
    }

    /// <summary>
    /// Advances the game to the next round, increasing difficulty and resetting round-specific counters.
    /// </summary>
    void AdvanceRound()
    {
        StartCoroutine(AdvanceRoundRoutine());
    }

    private IEnumerator AdvanceRoundRoutine()
    {
        Debug.Log("Round complete! Next round starting in 5 seconds...");

        yield return new WaitForSeconds(5f);

        currentRound++;
        zombieKills = 0;
        zombiesSpawned = 0;
        activeZombies = 0;
        killsToNextRound = Mathf.CeilToInt(killsToNextRound * 1.1f);

        healthMultiplier += 0.05f;
        damageMultiplier += 0.01f;
        spawnRateMultiplier += 0.02f;

        Debug.Log($"Round {currentRound} started! Health x{healthMultiplier}, Damage x{damageMultiplier}, SpawnRate x{spawnRateMultiplier}");
        FindAnyObjectByType<UI>()?.UpdateRoundText();
    }

    /// <summary>
    /// Gets the maximum number of zombies allowed to spawn in the current round.
    /// </summary>
    /// <returns>The maximum number of zombies for the current round.</returns>
    public int GetMaxZombiesThisRound()
    {
        return killsToNextRound;
    }
}