using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour {

	private GameObject player;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")){
			player = other.gameObject;
		}
	}

	void Update(){
		if( player != null){
			PlayerHealth health = player.GetComponent<PlayerHealth>();
			if(health.currentHealth < health.startingHealth){
				IntegrationTest.Pass(gameObject);
			} else {
				IntegrationTest.Fail(gameObject);
			}
		}
	}
}
