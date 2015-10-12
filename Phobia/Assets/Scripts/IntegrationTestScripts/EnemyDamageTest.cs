using UnityEngine;
using System.Collections;

public class EnemyDamageTest : MonoBehaviour {

	private GameObject player;
	private float nextTime;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Player")){
			player = other.gameObject;
			nextTime = Time.time + 1f;
		}
	}

	void Update(){
		if( player != null && Time.time > nextTime){
			PlayerHealth health = player.GetComponent<PlayerHealth>();
			if(health.currentHealth < health.startingHealth){
				IntegrationTest.Pass(gameObject);
			} else {
				IntegrationTest.Fail(gameObject);
			}
		}
	}
}
