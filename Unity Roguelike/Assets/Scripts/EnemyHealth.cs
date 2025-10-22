using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private HealthBarUI healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBarUI>();
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth, maxHealth);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} died!");

        PlayerController[] players = Object.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);

        foreach (PlayerController pc in players)
        {
            if (pc.GetTarget() == this)

            {
                pc.ClearTarget();
            }
        }

        Destroy(gameObject);
    }
}
