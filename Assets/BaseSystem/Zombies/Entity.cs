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
                FindObjectOfType<GameManager>().RegisterKill(); // Register kill in GameManager
                FindObjectOfType<UI>()?.UpdatePointsText();
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        Health = StartHealth * FindObjectOfType<GameManager>().healthMultiplier;
        playerCharacter = GameObject.FindWithTag("Player");
    }
}
