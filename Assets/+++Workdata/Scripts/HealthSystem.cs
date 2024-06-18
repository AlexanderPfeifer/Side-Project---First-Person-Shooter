using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    private int currentHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        HealthUIUpdate();
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void HealthUIUpdate()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
