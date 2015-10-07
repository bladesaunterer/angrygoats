using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles damaging logic between 
 * player to enemy (and vice-versa).
 * 
 **/
public static class HealthControl {
	
	public static void dealDamageToEnemy (GameObject other) {
		
		// Try and find an EnemyHealth script on the gameobject hit.
		EnemyHealth enemyHealth = other.gameObject.GetComponent <EnemyHealth> ();
		
		// If the EnemyHealth component exist...
		if(enemyHealth != null)
		{
			// ... the enemy should take damage, depending on type of attack.
			if (other.gameObject.CompareTag("SpecialAttack")) {
				enemyHealth.TakeDamage (50);
			} else {
				enemyHealth.TakeDamage (25);
			}
		}
	}

	public static void dealDamageToPlayer (GameObject other, int damage) {
		
		// Try and find an EnemyHealth script on the gameobject hit.
		PlayerHealth playerHealth = other.gameObject.GetComponent <PlayerHealth> ();
		
		// If the PlayerHealth component exist...
		if (playerHealth != null)
		{
			// ... the player should take damage.
			playerHealth.TakeDamage(damage);
		}
	}
	
}
