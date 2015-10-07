using UnityEngine;
using System.Collections;

public class EnemyPush : MonoBehaviour {
	public int knockback;
	// Use this for initialization
	void Start () {
	
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			if (other.gameObject.GetComponent<Rigidbody>() != null) {
				Debug.Log("Here!");
				other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward*knockback);
			}
		} 
	}
}
