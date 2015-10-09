using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles damaging logic between 
 * player to enemy (and vice-versa).
 * 
 **/
public static class HealthControl {
	
	public static void dealDamageToEnemy (GameObject other, int damage) {
		
		// Try and find an EnemyHealth script on the gameobject hit.
		EnemyHealth enemyHealth = other.gameObject.GetComponent <EnemyHealth> ();
		
		// If the EnemyHealth component exist the enemy should take damage,
		if (enemyHealth != null) {
			Debug.Log ("Enemy has health!");
			// ... the enemy should take damage.
			enemyHealth.TakeDamage (damage);
		} else {
			Debug.Log ("HAHAHAHA!");
		}
	}

	public static void dealDamageToPlayer (GameObject other, int damage) {
		
		// Try and find an EnemyHealth script on the gameobject hit.
		PlayerHealth playerHealth = other.gameObject.GetComponent <PlayerHealth> ();
		
		// If the PlayerHealth component exist...
		if (playerHealth != null) {
			Debug.Log ("Player has health!");
			// ... the player should take damage.
			playerHealth.TakeDamage (damage);
		} else {
			Debug.Log ("HAHAHAHA!");
		}
	}
	
}
