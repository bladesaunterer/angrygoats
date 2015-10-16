using UnityEngine;
using System.Collections;

/*
 * Tests that the enemy does the correct amount of damage to the player
 * Attach this to the enemy game object
 */
public class EnemyDamageTest : MonoBehaviour {

	private GameObject player;
	private float nextTime;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")){
			player = other.gameObject;
			nextTime = Time.time + 0.25f;
		}
	}

	void Update(){
		if( player != null && Time.time > nextTime){
			PlayerHealth health = player.GetComponent<PlayerHealth>();
			int damage = gameObject.GetComponent<EnemyAttack>().damage;
			float newHealth = health.startingHealth - damage;
			if(health.currentHealth == newHealth){
				IntegrationTest.Pass(gameObject);
			} else {
				IntegrationTest.Fail(gameObject);
			}
		}
	}
}
