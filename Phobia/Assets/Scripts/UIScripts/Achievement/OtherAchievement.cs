using UnityEngine;
using System.Collections;

public class OtherAchievement : GenericAchievement{

	// Use this for initialization
	void Start () {
		checkUnlocked();
		UpdateUI();
	}

}
