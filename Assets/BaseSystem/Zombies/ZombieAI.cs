using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player; // The player's transform
    public float attackRange = 2.0f; // Distance to start attacking
    public float damage = 10.0f;
    public float attackCooldown = 1.5f; // Time between attacks
    public GameObject playerCharacter; // Reference to the player character

    private NavMeshAgent agent; // NavMeshAgent component
    private float lastAttackTime; // Time of the last attack
    private ZombieAudio zombieAudio; // Reference to ZombieAudio component

    /// <summary>
    /// Initialize the ZombieAI script.
    /// </summary>
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerCharacter = GameObject.FindWithTag("Player");
        zombieAudio = GetComponent<ZombieAudio>();

        float multiplier = FindAnyObjectByType<GameManager>().damageMultiplier;
        damage *= multiplier;
    }

    /// <summary>
    /// Update is called once per frame to control the zombie's behavior.
    /// </summary>
    void Update()
    {
        if (player == null) return; // No player to follow

        float distance = Vector3.Distance(transform.position, player.position); // Get distance to player

        if (distance > attackRange)
        {
            agent.SetDestination(player.position); // Set destination for player
        }
        else
        {
            agent.ResetPath();
            if (Time.time > lastAttackTime + attackCooldown)
            {
                AttackPlayer();
                lastAttackTime = Time.time;
            }
        }
    }

    /// <summary>
    /// Attack the player and apply damage.
    /// </summary>
    void AttackPlayer()
    {
        if (playerCharacter != null)
        {
            zombieAudio.PlayAttackSound();
            playerCharacter.GetComponent<Player>().TakeDamage(damage);
        }
        else
        {
            Debug.LogWarning("Player character not found for attacking.");
        }
    }
}
