using UnityEngine;
using System.Collections;

public class PreLevelSelectSetup : MonoBehaviour
{

	public Level defaultFirstLevel;
	// Use this for initialization
	void Awake ()
	{
		LevelManager.Instance.firstGameSetup (defaultFirstLevel);	
	}

}
