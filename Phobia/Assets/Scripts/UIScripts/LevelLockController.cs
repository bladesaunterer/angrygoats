using UnityEngine;
using System.Collections;

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
