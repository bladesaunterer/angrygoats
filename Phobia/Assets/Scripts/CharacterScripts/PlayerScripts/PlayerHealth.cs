using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Purpose: Handles player health logic; including death.<para/>
/// Authors: Chester Booker, Karen Xie, Dean Ulbrick
/// </summary>
public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;            // The amount of health the player starts with.
	public int currentHealth;                   // The current health the player has.
	public int lethalLow;                       // The y co-ordinate of the world which causes the character to die.
	public Slider healthSlider;                 // Slider for player's health.
	private bool godMode;
	public int healthRegen = 5;
	public float perSecond = 1f;
	public float secDamage = 5f;
	bool hasTakenDamage = false;
	float dmgTimer;
	float timer;

	private PlayerControl playerControlScript;

	void Awake ()
	{
		// Setting the current health when the player first spawns.
		currentHealth = startingHealth;
		playerControlScript = GetComponent<PlayerControl> ();
		godMode = false;
	}

	void Update ()
	{
		timer += Time.deltaTime;
		// Damage player if they fall below a y.axis value
		if (gameObject.transform.position.y < lethalLow && currentHealth > 0) {
			TakeDamage (startingHealth);
		}

		if (Input.GetKeyDown (KeyCode.G)) {
            godMode = true;
            if (gameObject.GetComponent<PlayerControl>() != null) {
                gameObject.GetComponent<PlayerControl>().speed = 12;
            }
        }
			


		if (godMode) {
			currentHealth = 100;
			healthSlider.value = 100;
		}

		if (hasTakenDamage) {
			if (dmgTimer <= 0){
				hasTakenDamage = false;
			}else{
				dmgTimer -= Time.deltaTime;
			}
			
		} else {
			if (timer > perSecond) {
				timer = 0f;
				HealPlayer (healthRegen);
			}
		}

	}

	public void TakeDamage (int amount)
	{
		// Reduce the current health by the amount of damage sustained.
		currentHealth -= amount;

		if (amount != 0) {
			hasTakenDamage = true;
			dmgTimer = secDamage;
		}

		// Update health on slider to new value.
		healthSlider.value = currentHealth;

		// It there is no health left.
		if (currentHealth <= 0) {
			// Play sound, animate and destroy object.
			PlayerSfxScript.playDeathSound ();
			playerControlScript.InitiateAnimation ("Die");
			Destroy (gameObject, 0.95f);
		} else {
			// Play sound and animate.
			PlayerSfxScript.playHitSound ();
			playerControlScript.InitiateAnimation ("Hit");
		}
	}

	// Used by the healing gem's ability
	public void HealPlayer (int heal)
	{
		currentHealth += heal;
		if (currentHealth > 100) {
			currentHealth = 100;
		}
		healthSlider.value = currentHealth;
	}
}
