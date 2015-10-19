using UnityEngine;
using System.Collections;

/**
 * Runs in scene prior to level select to set up which levels will be locked or unlocked
 */
public class PreLevelSelectSetup : MonoBehaviour
{

	public Level defaultFirstLevel;
	// Use this for initialization
	void Awake ()
	{
		//use to test
		//LevelManager.Instance.resetLevels ();
		LevelManager.Instance.firstGameSetup (defaultFirstLevel);	
	}

}
