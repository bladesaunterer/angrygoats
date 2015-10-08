﻿using UnityEngine;
using System.Collections;

public class EnemyCharge : MonoBehaviour {

	int flag = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		if (Physics.Raycast (transform.position, fwd, 10)) {
				this.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * 100);
		}
	}
}
