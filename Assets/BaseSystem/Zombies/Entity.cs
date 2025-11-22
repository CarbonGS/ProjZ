using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float StartHealth;
    public float health;
    public GameObject playerCharacter;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                playerCharacter.GetComponent<Player>().points += 100;
                FindAnyObjectByType<GameManager>().RegisterKill(); // Register kill in GameManager
                FindAnyObjectByType<FPUI>()?.UpdatePointsText();
                Destroy(gameObject);
            }
        }
    }

    /// <summary>
    /// Initializes the entity's health and finds the player character on start.
    /// </summary>
    void Start()
    {
        Health = StartHealth * FindAnyObjectByType<GameManager>().healthMultiplier;
        playerCharacter = GameObject.FindWithTag("Player");
    }

    /// <summary>
    /// Updates the entity's state every frame. (Describe specific updates if known)
    /// </summary>
    void Update()
    {
        // Add any per-frame logic here
    }
}
