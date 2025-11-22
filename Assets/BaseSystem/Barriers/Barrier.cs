using UnityEngine;
using TMPro;

public class Barrier : MonoBehaviour
{
    public float buyValue; // Cost to buy the barrier
    public TMP_Text notificationText;
    public AudioSource audioSource;
    public AudioClip buySound;

    private GameObject playerCharacter; // Reference to the player character
    private BoxCollider buyZone; // box collider sub component for buying
    private bool CanBuy = false;

    /// <summary>
    /// Initialize player character and buy zone on start.
    /// </summary>
    void Start()
    {
        playerCharacter = GameObject.FindWithTag("Player");
        buyZone = GetComponent<BoxCollider>();
        buyZone.isTrigger = true; // Ensure the collider is set as a trigger
    }

    /// <summary>
    /// Check for player entering the trigger zone to enable buying.
    /// </summary>
    /// <param name="other">The collider of the object that entered the trigger.</param>
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

    /// <summary>
    /// Check for player exiting the trigger zone to disable buying.
    /// </summary>
    /// <param name="other">The collider of the object that exited the trigger.</param>
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

    /// <summary>
    /// Updates every frame to check if the player tries to buy the barrier.
    /// </summary>
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

    /// <summary>
    /// Handles the barrier purchase process.
    /// </summary>
    /// <param name="player">The player purchasing the barrier.</param>
    public void BuyBarrier(Player player)
    {
        if (audioSource != null && buySound != null)
        {
            audioSource.PlayOneShot(buySound);
        }
        player.points -= (int)buyValue;
        notificationText.text = "";
        FindAnyObjectByType<FPUI>()?.UpdatePointsText();
        Destroy(gameObject); // Remove barrier after purchase
        Debug.Log("Barrier purchased for " + buyValue + " points. Remaining points: " + player.points);
    }
}
