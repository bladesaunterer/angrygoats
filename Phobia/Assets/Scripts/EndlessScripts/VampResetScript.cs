using UnityEngine;
using System.Collections;

public class VampResetScript : MonoBehaviour {

	// AHHHH VAMPIRES! D;
	GameObject ramp;
	// Use this for initialization
	public void reset () {
		ramp = GameObject.Find ("Ramp");
		ramp.GetComponent<Ramp> ().times = 0;
		PlayerPrefs.SetInt ("endlessPreviousScore", 0);
	}
}