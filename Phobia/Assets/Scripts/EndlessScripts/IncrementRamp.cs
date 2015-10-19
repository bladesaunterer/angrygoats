using UnityEngine;
using System.Collections;
/**
 * Class to handle incrementing number of levels played on the master controller object.
 * As script to allow for attachment to UI components.
 */
public class IncrementRamp : MonoBehaviour {

	public GameObject ramp;
	void Start () {
		/**
	 	* Finds the relevant scene object and increments the appropriate field.
	 	*/
		ramp = GameObject.Find ("Ramp");
		ramp.GetComponent<Ramp> ().times++;
	}
}
