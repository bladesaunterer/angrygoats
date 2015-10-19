using UnityEngine;
using System.Collections;

/**
 * Class to hard reset all progress on the master controller object to avoid overlap.
 * As script to allow for attachment to UI components.
 */
public class VampResetScript : MonoBehaviour {

	// AHHHH VAMPIRES! D;
	GameObject ramp;
	/**
	 * Finds the relevant scene object and wipes the appropriate field.
	 */
	public void reset () {
		ramp = GameObject.Find ("Ramp");
		ramp.GetComponent<Ramp> ().times = 0;
		PlayerPrefs.SetInt ("endlessPreviousScore", 0);
	}
}