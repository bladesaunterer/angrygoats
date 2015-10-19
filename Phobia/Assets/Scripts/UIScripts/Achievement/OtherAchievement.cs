using UnityEngine;
using System.Collections;

/**
 * Helper class for checking other achievements
 */
public class OtherAchievement : GenericAchievement{

	// Use this for initialization
	void Start () {
		checkUnlocked();
		UpdateUI();
	}

}
