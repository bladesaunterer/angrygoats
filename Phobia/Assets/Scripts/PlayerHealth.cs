using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the enemy starts the game with.
	public int currentHealth;                   // The current health the enemy has.
	public Slider healthSlider;
	
	void Awake ()
	{		
		// Setting the current health when the enemy first spawns.
		currentHealth = startingHealth;
	}

	public void TakeDamage (int amount)
	{		
		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;
		//healthSlider.value = currentHealth;
		
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			// ... the enemy is dead.
			Destroy (gameObject);
		}
	}
}
