using UnityEngine;
using System.Collections;

public class AttackControl : MonoBehaviour {

	private Rigidbody rb;
	public  float meleeTimeout;
	public float attackSpeed;

	void Start(){
		rb = GetComponent<Rigidbody> ();
		Destroy(gameObject, meleeTimeout);
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Door") || other.gameObject.CompareTag ("Wall")) {
			Destroy(gameObject);
		} 
	}
}
