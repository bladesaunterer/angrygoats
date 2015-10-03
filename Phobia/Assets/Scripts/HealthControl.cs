using UnityEngine;
using System.Collections;

public static class HealthControl {
	
	public static void dealDamageToEnemy (GameObject other) {
		
		// Try and find an EnemyHealth script on the gameobject hit.
		EnemyHealth enemyHealth = other.gameObject.GetComponent <EnemyHealth> ();
		
		// If the EnemyHealth component exist...
		if(enemyHealth != null)
		{
			if (other.gameObject.CompareTag("SpecialAttack")) {
			// ... the enemy should take damage.
				enemyHealth.TakeDamage (50);
			} else {
				enemyHealth.TakeDamage (25);
			}
		}
	}

	public static void dealDamageToPlayer (GameObject other) {
		
		// Try and find an EnemyHealth script on the gameobject hit.
		PlayerHealth playerHealth = other.gameObject.GetComponent <PlayerHealth> ();
		
		// If the EnemyHealth component exist...
		if (playerHealth != null)
		{
			playerHealth.TakeDamage(8);
		}
	}
}
