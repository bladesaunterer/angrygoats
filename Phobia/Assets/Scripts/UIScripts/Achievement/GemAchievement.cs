using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GemAchievement : GenericAchievement {
	
	private GemManager gemManager = new GemManager();

	public Gem achievement;

	// Loads what gems have been locked or unlocked
	void Start () {
		unlocked = gemManager.CheckIfGemUnlocked(achievement);
		UpdateUI();
	}

}
