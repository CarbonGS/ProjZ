using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI pointsText;
    private GameManager gameManager;
    public GameObject player;
    public MainMenuManager mmm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameManager = FindObjectOfType<GameManager>();
        UpdateRoundText();
        UpdatePointsText();
    }

    public void UpdateRoundText()
    {
        if (gameManager != null && roundText != null)
        {
            roundText.text = gameManager.currentRound.ToString();
        }
    }

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

    // Update is called once per frame
    void Update()
    {

    }
}
