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

	private bool gemUsage;

	void Start(){
		gemUsage = false;
	}

	/**
	 * Checks for achievements that are earned when you beat a level
	 */
	public void OnLevelEnd(){
		int enemiesCount = TEMPScoreScript.Instance.enemyCounter;
		int totalEnemies = generator.totalEnemies;
		if (enemiesCount == 0){

		}
		if (enemiesCount == totalEnemies){

		}
		if (!gemUsage){

		}
	}

	/**
	 * Checks for achievements when the player dies
	 */
	public void OnLevelLoss(){
		int enemiesCount = TEMPScoreScript.Instance.enemyCounter;
		if (enemiesCount == 0){

		}
	}

	/**
	 * When the user uses a gem
	 */
	public void usedGem(){
		gemUsage = true;
	}
}
