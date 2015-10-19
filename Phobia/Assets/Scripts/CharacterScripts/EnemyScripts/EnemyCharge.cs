using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Applies a charge ability to certain enemies to give them super speed abilities.<para/>
/// Authors:
/// </summary>
public class EnemyCharge : MonoBehaviour
{
	public int scale = 100;
	public float initDelay = 0;
	
	private bool hasInit = false;
	private float initTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (hasInit && Time.time > initTime + initDelay) {
			GetComponent<AIPath>().enabled = true;
			Vector3 fwd = transform.TransformDirection (Vector3.forward);
			if (Physics.Raycast (transform.position, fwd)) {
					this.gameObject.GetComponent<Rigidbody> ().AddForce (gameObject.transform.forward * scale);
			}
		}
	}
	
	public void StartCharge() {
		if (!hasInit) {
			hasInit = true;
			initTime = Time.time;
		}
	}
}
