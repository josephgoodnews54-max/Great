using UnityEngine;

public class Target : MonoBehaviour
{
    public int maxHealth = 100;
    public int scoreValue = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        GameManager.Instance.AddScore(scoreValue);
        Destroy(gameObject);
    }
}
