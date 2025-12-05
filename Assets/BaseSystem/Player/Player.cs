using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Player health management
    private float maxHealth = 100f; // Maximum health
    private float currentHealth; // Current health
    private float targetHealth; // Target health for smooth transitions
    private float timeSinceLastDamage = 0f; // Time since last damage taken
    private bool isHealing = false; // Flag to indicate if healing is in progress
    public float healRate = 5f; // Health points healed per second
    private Slider healthBar; // UI Slider to represent health (reference assigned in Start)

    // Player points management
    public int points = 100; // Player points (100 for default)

    // Player Audio
    public AudioSource playerAudio; // Audio source for player sounds
    public AudioClip dieSound; // Sound played on player death

    // UI Management
    private GameObject pauseMenu; // Reference to the pause menu UI

    /// <summary>
    /// Initializes the player object, setting up references and initial health values.
    /// </summary>
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>(); // Find health bar UI
        currentHealth = maxHealth; // Initialize current health
        targetHealth = maxHealth; // Initialize target health
    }


    /// <summary>
    /// Updates the player state each frame, handling health regeneration and UI updates.
    /// </summary>
    void Update()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, targetHealth / maxHealth, Time.deltaTime * 5); 

        timeSinceLastDamage += Time.deltaTime;
        if (timeSinceLastDamage >= 5f && currentHealth < maxHealth)
        {
            isHealing = true;
        }
        if (isHealing)
        {
            currentHealth += healRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            targetHealth = currentHealth;
            if (currentHealth >= maxHealth)
            {
                isHealing = false; // Stop healing when at max health
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

        }
    }

    /// <summary>
    /// Inflicts damage to the player, reducing current health and handling death if health falls to zero or below.
    /// </summary>
    /// <param name="amount">The amount of damage to inflict.</param>
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        targetHealth = currentHealth;

        timeSinceLastDamage = 0f; // Reset timer
        isHealing = false; // Stop healing when damaged

        Debug.Log("Player took damage: " + amount + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Handles the player's death, playing a death sound and quitting the game after a delay.
    /// </summary>
    void Die()
    {
        Debug.Log("Player has died.");

        // Pause the game
        Time.timeScale = 0f;

        if (playerAudio != null && dieSound != null)
        {
            playerAudio.PlayOneShot(dieSound);
            StartCoroutine(WaitForDeathAudio());
        }
        else
        {
            QuitGame(); // fallback
        }
    }

    /// <summary>
    /// Coroutine that waits for the death audio to finish playing before quitting the game.
    /// </summary>
    IEnumerator WaitForDeathAudio()
    {
        // Wait in real time, not game time
        yield return new WaitForSecondsRealtime(dieSound.length);
        QuitGame();
    }

    /// <summary>
    /// Quits the game or stops play mode in the editor.
    /// </summary>
    void QuitGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
