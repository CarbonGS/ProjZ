using UnityEngine;
using TMPro;

public class Barrier : MonoBehaviour
{
    public float buyValue; // Cost to buy the barrier
    private GameObject playerCharacter; // Reference to the player character
    private BoxCollider buyZone; // box collider sub component for buying
    private bool CanBuy = false;
    public TMP_Text notificationText; // Assign in inspector

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
            if (notificationText != null)
            {
               notificationText.text = $"Press 'F' to buy barrier for {buyValue} points";
            }
        }
           
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == playerCharacter)
        {
            CanBuy = false;
            if (notificationText != null)
            {
                notificationText.text = "";
            }
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
        notificationText.text = "";
        Destroy(gameObject); // Remove barrier after purchase
        Debug.Log("Barrier purchased for " + buyValue + " points. Remaining points: " + player.points);
    }
}
