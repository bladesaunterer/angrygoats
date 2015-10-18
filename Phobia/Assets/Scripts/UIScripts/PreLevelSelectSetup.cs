using UnityEngine;
using System.Collections;

public class PreLevelSelectSetup : MonoBehaviour
{

	public Level defaultFirstLevel;
	// Use this for initialization
	void Awake ()
	{
		//uncomment to reset levels
//		LevelManager.Instance.lockLevel (Level.Endless);
//		LevelManager.Instance.lockLevel (Level.Height);
//		LevelManager.Instance.lockLevel (Level.Dark);
		LevelManager.Instance.firstGameSetup (defaultFirstLevel);	
	}

}
