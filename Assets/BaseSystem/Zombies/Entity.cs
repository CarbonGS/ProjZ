using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public float StartHealth;
    public float health;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        Health = StartHealth;
    }
}
