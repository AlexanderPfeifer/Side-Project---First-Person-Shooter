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

    //Takes damage and when hp is at zero, then dies
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        HealthUIUpdate();
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    //Updates the health ui of the player
    void HealthUIUpdate()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }

    //Object gets destroyed when no hp is left
    private void Die()
    {
        if (TryGetComponent(out CharacterMovement characterMovement))
        {
            GameController.Instance.LooseGame();
        }
        
        Destroy(gameObject);
    }
}
