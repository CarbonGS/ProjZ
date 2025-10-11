using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health
    public float currentHealth; // Current health
    public Slider healthBar; // UI Slider to represent health

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

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        healthBar.value = currentHealth / maxHealth;
    }

    void Die()
    {
        // Handle player death (e.g. restart level, show game over screen)
    }
}
