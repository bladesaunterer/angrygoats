using UnityEngine;
using System.Collections;

public class FireDamage : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		GameObject enemy = other.gameObject;
		EnemyHealth health = enemy.GetComponent<EnemyHealth>();
		if(health.currentHealth < health.startingHealth){
			IntegrationTest.Pass(gameObject);
		} else {
			IntegrationTest.Fail(gameObject);
		}
	}
}
