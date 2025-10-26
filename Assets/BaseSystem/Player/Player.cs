using UnityEngine;
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthBar = GameObject.Find("HealthBar").GetComponent<Slider>(); // Find health bar UI
        currentHealth = maxHealth; // Initialize current health
        targetHealth = maxHealth; // Initialize target health
    }

    // Update is called once per frame
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
    }

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

    void Die()
    {
        // Quits game (debug mode only)
        Debug.Log("Player has died.");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
}
