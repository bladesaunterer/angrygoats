using UnityEngine;
using System.Collections;

/*
 * This tests the initial damange of the gems. 
 * Attach this script to the bolt.
 */
public class InitialDamageTest : MonoBehaviour {

	private GemManager gemSelection = new GemManager ();

	void OnTriggerEnter (Collider other)
	{
		GameObject enemy = other.gameObject;
		EnemyHealth health = enemy.GetComponent<EnemyHealth>();
		Gem currentGem = gemSelection.GetCurrentGem ();
		GameObject gemObject = GameObject.FindGameObjectWithTag (currentGem.ToString ());
		int damage = gemObject.GetComponent<GenericGem>().damage;

		int newHealth = health.startingHealth - damage;

		if(health.currentHealth == newHealth){
			gemObject.GetComponent<GemEffectTest>().toggleOnHit(other.gameObject);
		} else {
			Debug.Log("fail");
			IntegrationTest.Fail(gameObject);
		}
	}
	
}
