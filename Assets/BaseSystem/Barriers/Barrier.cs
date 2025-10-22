using UnityEngine;

public class Barrier : MonoBehaviour
{
    public float buyValue; // Cost to buy the barrier
    private GameObject playerCharacter; // Reference to the player character
    private BoxCollider buyZone; // box collider sub component for buying
    private bool CanBuy = false;

    void Start()
    {
        playerCharacter = GameObject.FindWithTag("Player");
        buyZone = GetComponent<BoxCollider>();
        buyZone.isTrigger = true; // Ensure the collider is set as a trigger
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == playerCharacter)
        {
            CanBuy = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCharacter)
        {
            CanBuy = false;
        }
    }

    void Update()
    {
        if(CanBuy && Input.GetKeyDown(KeyCode.F))
        {
            Player player = playerCharacter.GetComponent<Player>();
            if (player.points >= buyValue)
            {
                BuyBarrier(player);
            }
            else
            {
                Debug.Log("Not enough points to buy the barrier. Required: " + buyValue + ", Available: " + player.points);
            }
        }
    }

    public void BuyBarrier(Player player)
    {
        player.points -= (int)buyValue;
        Destroy(gameObject); // Remove barrier after purchase
        Debug.Log("Barrier purchased for " + buyValue + " points. Remaining points: " + player.points);
    }
}
