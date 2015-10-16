﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Handles damaging logic between player to enemies(and vice-versa).<para/>
/// Authors:
/// </summary>
public static class HealthControl {
	
	public static void dealDamageToEnemy (GameObject other, int damage) {
		
		// Try and find EnemyHealth script on the gameobject hit.
		EnemyHealth enemyHealth = other.gameObject.GetComponent <EnemyHealth> ();
		
		// If the EnemyHealth component exist the enemy should take damage,
		if (enemyHealth != null && enemyHealth.currentHealth >= 0) {

			Debug.Log ("Enemy has health!");

			// Enemy takes damage.
			enemyHealth.TakeDamage (damage);

		} else {
			Debug.Log ("No enemy detected || No enemy health");
		}
	}

	public static void dealDamageToPlayer (GameObject other, int damage) {
		
		// Try and find EnemyHealth script on the gameobject hit.
		PlayerHealth playerHealth = other.gameObject.GetComponent <PlayerHealth> ();

        // If the EnemyHealth component exist the enemy should take damage
        if (playerHealth != null && playerHealth.currentHealth >= 0) {

			Debug.Log ("Player has health!");

			// Player takes damage.
			playerHealth.TakeDamage (damage);

		} else {
            Debug.Log("No player detected || No player health");
        }
	}	
}
