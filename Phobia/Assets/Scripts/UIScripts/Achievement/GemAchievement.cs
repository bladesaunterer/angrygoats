using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * Script for managing achievements based on gems unlocked
 */
public class GemAchievement : GenericAchievement {
	
	private GemManager gemManager = GemManager.Instance;
	public Gem achievement;

	// Loads what gems have been locked or unlocked
	void Start () {
		unlocked = gemManager.CheckIfGemUnlocked(achievement);
		UpdateUI();
	}

}
