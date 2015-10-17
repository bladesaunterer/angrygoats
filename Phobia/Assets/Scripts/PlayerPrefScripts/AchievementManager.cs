using System.Linq;
using UnityEngine;
using System.Collections;

// AchievementManager contains Achievements, which players are able to earn through performing various actions
// in the game. Each Achievement specifies 

[System.Serializable]
public class Achievement
{
	public string Name;
	public string Description;
	//public Texture2D IconIncomplete;
	//public Texture2D IconComplete;
	public int TargetProgress;
	public bool Secret;
	
	[HideInInspector]
	public bool Earned = false;
	public int currentProgress = 0;
	
	// Returns true if this progress added results in the Achievement being earned.
	public bool AddProgress(int progress)
	{
		if (Earned)
		{
			return false;
		}
		currentProgress += progress;
		if (currentProgress >= TargetProgress)
		{
			Earned = true;
			return true;
		}	
		return false;
	}
}

/**
 * Singleton class to keep track of achievements
 */
public class AchievementManager : MonoBehaviour
{
	private static AchievementManager instance;

	public static AchievementManager Instance{
		get{
			if (instance == null) {
				instance = new AchievementManager ();
			}
			return AchievementManager.instance;
		}
	}

	public Achievement[] achievements;
	//public AudioClip EarnedSound;
		
	void Start()
	{
		LoadAchievements();
	}
	
	private void LoadAchievements(){
		foreach (Achievement achievement in achievements){
			int progress = PlayerPrefs.GetInt(achievement.Name);
			AddProgressToAchievement(achievement.Name,progress);
		}
	}
	
	private Achievement GetAchievementByName(string achievementName)
	{
		return achievements.FirstOrDefault(achievement => achievement.Name == achievementName);
	}

	/**
	 * Plays a sound when an achievement is earned?
	 * Need to update UI? or should like UI find this...?
	 */
	private void AchievementEarned()
	{
		//AudioSource.PlayClipAtPoint(EarnedSound, Camera.main.transform.position);        
	}
	
	public void AddProgressToAchievement(string achievementName, int progressAmount)
	{
		Achievement achievement = GetAchievementByName(achievementName);
		if (achievement == null)
		{
			Debug.LogWarning("AchievementManager::AddProgressToAchievement() - Trying to add progress to an achievemnet that doesn't exist: " + achievementName);
			return;
		}
	
		if (achievement.AddProgress(progressAmount))
		{
			AchievementEarned();
		}
	}

	/**
	 * Save all the achievement progress.
	 */
	void OnApplicationQuit() {
		foreach (Achievement achievement in achievements){
			PlayerPrefs.SetInt(achievement.Name,achievement.currentProgress);
		}
		PlayerPrefs.Save();
	}
}