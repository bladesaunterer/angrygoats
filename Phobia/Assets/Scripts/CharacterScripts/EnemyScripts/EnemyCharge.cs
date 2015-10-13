using UnityEngine;
using System.Collections;

public class EnemyCharge : MonoBehaviour {

	public int scale = 100;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast (transform.position, fwd)) {
				this.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * scale);
		}
	}
}
