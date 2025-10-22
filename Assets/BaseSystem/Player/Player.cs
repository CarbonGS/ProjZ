using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float maxHealth = 100f; // Maximum health
    private float currentHealth; // Current health
    public Slider healthBar; // UI Slider to represent health
    public int points = 100; // Player points (100 for default)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth; // Initialize current health
        UpdateHealthUI(); // Update the health UI

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
        Debug.Log("Player took damage: " + amount + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        // healthBar.value = currentHealth / maxHealth;
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
