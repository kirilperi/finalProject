using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance { get; private set; } // Singleton Instance

    public Slider healthSlider;
    public float maxHealth = 100f;
    public float health;
    float lerpSpeed = 0.05f;
    public bool isAlive => health > 0;

    private void Awake()
    {
        // Singleton Initialization
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        health = maxHealth; // Initialize health
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    private void Update()
    {
        // Smoothly update the health slider
        if (healthSlider.value != health)
        {
            healthSlider.value = Mathf.Lerp(healthSlider.value, health, lerpSpeed);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }
}
