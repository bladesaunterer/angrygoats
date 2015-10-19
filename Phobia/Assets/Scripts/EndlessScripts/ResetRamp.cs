using UnityEngine;
using System.Collections;

public class ResetRamp : MonoBehaviour {
	
	public GameObject ramp;
	// Use this for initialization
	void Start () {
		ramp = GameObject.Find ("Ramp");
		ramp.GetComponent<Ramp> ().times = 0;
	}
}
