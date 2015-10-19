using UnityEngine;
using System.Collections;

public class Ramp : MonoBehaviour {

	public float ramp = 1.25f; // It's like compound interest except there's no money involved.
	public int times = 0;
	// Makes sure this persists across loads.
	void Start () {
		DontDestroyOnLoad (this);
		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		}
	}
}
