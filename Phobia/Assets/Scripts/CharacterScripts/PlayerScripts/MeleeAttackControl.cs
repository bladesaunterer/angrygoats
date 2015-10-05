using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles player melee attack logic.
 * 
 **/
public class MeleeAttackControl : MonoBehaviour {

	private Rigidbody rb;
	public  float meleeTimeout;
	public float attackSpeed;

	void Start(){

		// Destroy projectile, to simulate a short-ranged melee attack.
		Destroy(gameObject, meleeTimeout);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Door") 
		    || other.gameObject.CompareTag ("Wall")
		    || other.gameObject.CompareTag ("Enemy")
		    || other.gameObject.CompareTag ("Boss")) {

			// Destroy bolt on contact.
			Destroy(gameObject);

			// If bolt hits an enemy, deal damage to that enemy.
			HealthControl.dealDamageToEnemy(other.gameObject);
		} 
	}
}
