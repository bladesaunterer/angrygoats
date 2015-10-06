using UnityEngine;
using System.Collections;

public class EnemyBolt : MonoBehaviour {

		public float speed;
		
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
		    || other.gameObject.CompareTag ("Boss") || other.gameObject.CompareTag ("Player")) {
				
				// Destroy bolt on contact.
				Destroy(gameObject);
				
				// If bolt hits an enemy, deal damage to that enemy.
				HealthControl.dealDamageToPlayer(other.gameObject, 50);
			}
		}
	}
