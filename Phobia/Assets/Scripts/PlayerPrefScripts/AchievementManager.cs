using UnityEngine;
using System.Collections;

public class AchievementManager : MonoBehaviour {

	private static AchievementManager instance;

	public static AchievementManager Instance{
		get{
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<AchievementManager>();
			}
			return AchievementManager.instance;
		}
	}

	public LevelGenerator generator;

    public bool disabled = false;

	private bool gemUsage;

	void Start(){
		gemUsage = false;
	}

	/**
	 * Checks for achievements that are earned when you beat a level
	 */
	public void OnLevelEnd(){
        if (disabled == true) {
            return;
        }
		int enemiesCount = TEMPScoreScript.Instance.enemyCounter;
		int totalEnemies = generator.totalEnemies;
		if (enemiesCount == 1){
			PlayerPrefs.SetInt("Only The Boss",1);
		}
		if (enemiesCount == totalEnemies + 1){
			PlayerPrefs.SetInt("Level Clear",1);
		}
		if (!gemUsage){
			PlayerPrefs.SetInt("You're Not Special",1);
		}
	}

	/**
	 * Checks for achievements when the player dies
	 */
	public void OnLevelLoss() {
        if (disabled == true) {
            return;
        }
        int enemiesCount = TEMPScoreScript.Instance.enemyCounter;
		if (enemiesCount == 0){
			PlayerPrefs.SetInt("You Suck",1);
		}
	}

	/**
	 * When the user uses a gem
	 */
	public void usedGem() {
        if (disabled == true) {
            return;
        }
        gemUsage = true;
	}
}
