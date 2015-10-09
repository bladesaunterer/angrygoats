using UnityEngine;
using UnityEngine.UI;

/**
 * 
 * Class which handles player health logic.
 * 
 **/
public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the player starts with.
	public int currentHealth;                   // The current health the player has.
	public Slider healthSlider;					// Slider for player's health.

	public GameObject deadScreen;
	private bool isShowingDead = false;

	void Awake ()
	{		
		// Setting the current health when the player first spawns.
		currentHealth = startingHealth;
	}

	public void TakeDamage (int amount)
	{		
		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		// Update health on slider to new value.
		if (healthSlider != null ) {
			healthSlider.value = currentHealth;
		}
		
		// If the current health is less than or equal to zero...
		if(currentHealth <= 0)
		{
			isShowingDead = !isShowingDead;
			deadScreen.SetActive(isShowingDead);

			// ... the player is destroyed.
			Destroy (gameObject);
		}
	}

	public void HealPlayer(int heal){
		currentHealth += heal;
		if (currentHealth > 100) {
			currentHealth = 100;
		}
		if (healthSlider != null ) {
			healthSlider.value = currentHealth;
		}
	}
}
