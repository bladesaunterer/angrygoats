using UnityEngine;
using System.Collections;

public class BoltMover : MonoBehaviour {
    
    public float speed;

	private Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Door") || other.gameObject.CompareTag ("Wall") || other.gameObject.CompareTag ("Enemy")) {
			Destroy(gameObject);

			HealthControl.dealDamageToEnemy(other.gameObject);
		}
	}
}
