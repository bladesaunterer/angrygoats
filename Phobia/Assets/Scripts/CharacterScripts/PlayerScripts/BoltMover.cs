using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles special attack (bolt) logic.
 * 
 **/
public class BoltMover : MonoBehaviour {
    
    public float speed;
	public int damage;

	private Rigidbody rb;

	void Start(){

		// Get rigidbody and set bolt's velocity.
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
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
			HealthControl.dealDamageToEnemy(other.gameObject, damage);
		}
	}
}
