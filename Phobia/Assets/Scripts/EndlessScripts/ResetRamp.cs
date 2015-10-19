using UnityEngine;
using System.Collections;
/**
 * Class to handle resetting number of levels played on the master controller object.
 * As script to allow for attachment to UI components.
 */
public class ResetRamp : MonoBehaviour {
	
	public GameObject ramp;
	// Use this for initialization
	void Start () {
		/**
	 	* Finds the relevant scene object and increments the appropriate field.
	 	*/
		ramp = GameObject.Find ("Ramp");
		ramp.GetComponent<Ramp> ().times = 0;
	}
}
