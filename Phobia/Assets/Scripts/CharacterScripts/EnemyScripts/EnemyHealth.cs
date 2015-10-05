using UnityEngine;

/**
 * 
 * Class which handles enemy health logic.
 * 
 **/
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.

    void Awake()
    {
        // Set the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        // Reduce current health by the amount of damage taken.
        currentHealth -= amount;

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0)
        {
            // ... the enemy is destroyed.
            Destroy(gameObject);

            Debug.Log("Enemy Destroyed!");
            if (this.tag == "Enemy")
            {
				// Increment score when destroyed.
                TEMPScoreScript.Instance.IncrementScore(10);
            }
        }
    }
}
