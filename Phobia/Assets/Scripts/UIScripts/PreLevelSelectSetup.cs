using UnityEngine;
using System.Collections;

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
