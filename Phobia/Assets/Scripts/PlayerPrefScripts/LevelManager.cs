using UnityEngine;
using System.Collections;

/**
 * Instance class to manage locking and unlocking of levels.
 */
public class LevelManager
{

	private static LevelManager instance;

	
	public static LevelManager Instance {
		get {
			if (instance == null) {
				instance = new LevelManager ();
			}
			return LevelManager.instance;
		}
	}

	/**
	 * Will register level as unlocked
	 */
	public void unlockLevel (Level level)
	{
		PlayerPrefs.SetString (level.ToString (), "Unlocked");
		PlayerPrefs.Save ();
	}

	/**
	 * Will register level as locked
	 */
	public void lockLevel (Level level)
	{
		PlayerPrefs.SetString (level.ToString (), "Locked");
		PlayerPrefs.Save ();
		
	}

	/**
	 * Returns true if level unlocked
	 */
	public bool checkLevelUnlocked (Level level)
	{
		if (PlayerPrefs.GetString (level.ToString ()).Equals ("Unlocked"))
			return true;
		else
			return false;
	}

	/**
	 * Will check if keys for level exist in player prefs
	 * and will set them up if no key exists
	 */
	public void firstGameSetup (Level level)
	{
		foreach (Level l in Level.GetValues(typeof(Level))) {
			if (l == level && !PlayerPrefs.HasKey (l.ToString ()))
				unlockLevel (l);
			else if (!PlayerPrefs.HasKey (l.ToString ()))
				lockLevel (l);
		}
	}


}
