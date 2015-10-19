using UnityEngine;
using System.Collections;

/**
 * Attached to object in ui containing buttons for level select(UnlockedLevel) 
 * and image for when the level is locked(LockedLevel). Will enable and disable 
 * objects based on whether or not level in unlocked.
 */
public class LevelLockController : MonoBehaviour
{
	public GameObject UnlockedLevel;
	public GameObject LockedLevel;
	
	public Level level;


	void Awake ()
	{
		LevelManager lm = LevelManager.Instance;

		if (lm.checkLevelUnlocked (level)) {
			UnlockedLevel.gameObject.SetActive (true);
			LockedLevel.gameObject.SetActive (false);
		} else {
			UnlockedLevel.gameObject.SetActive (false);
			LockedLevel.gameObject.SetActive (true);
		}
	}
	
}
