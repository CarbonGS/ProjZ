using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI pointsText;
    private GameManager gameManager;
    public GameObject player;
    public MainMenuManager mmm;

    /// <summary>
    /// Initializes the player and game manager references
    /// and updates the round and points text at the start.
    /// </summary>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = FindAnyObjectByType<GameManager>();
        UpdateRoundText();
        UpdatePointsText();
    }

    /// <summary>
    /// Updates the round text UI element to match the current round
    /// from the game manager.
    /// </summary>
    public void UpdateRoundText()
    {
        if (gameManager != null && roundText != null)
        {
            roundText.text = gameManager.currentRound.ToString();
        }
    }

    /// <summary>
    /// Updates the points text UI element to match the player's current points.
    /// </summary>
    public void UpdatePointsText()
    {
        if (player != null && pointsText != null)
        {
            Player playerScript = player.GetComponent<Player>();
            if (playerScript != null)
            {
                pointsText.text = playerScript.points.ToString();
            }
        }
    }

    /// <summary>
    /// This function is called every frame but is currently empty.
    /// </summary>
    // Update is called once per frame
    void Update()
    {

    }
}
