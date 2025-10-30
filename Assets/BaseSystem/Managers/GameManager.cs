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

    public bool CanSpawnZombie()
    {
        return zombiesSpawned < killsToNextRound && activeZombies < killsToNextRound;
    }

    public void RegisterSpawn()
    {
        zombiesSpawned++;
        activeZombies++;
    }

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

    void Start()
    {
        Debug.Log($"GameManager initialized. spawnRateMultiplier = {spawnRateMultiplier}");
    }

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
        FindObjectOfType<UI>()?.UpdateRoundText();
    }

    public int GetMaxZombiesThisRound()
    {
        return killsToNextRound;
    }
}