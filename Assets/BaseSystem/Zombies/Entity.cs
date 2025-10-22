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
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        Health = StartHealth;
        playerCharacter = GameObject.FindWithTag("Player");
    }
}
