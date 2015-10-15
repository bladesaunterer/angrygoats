using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles button logic which moves to different scenes.
 * 
 **/
public class ButtonNextLevel : MonoBehaviour
{
	private string level;

	//allows external gameobject to set level to be loaded
	public void SetLevel (string level)
	{
		Debug.Log ("level set");
		this.level = level;

	}

	//Will load scene based in index in build settings
	public void NextLevelButton (int index)
	{
		Application.LoadLevel (index);
	}

	//Will load scene based on name
	public void NextLevelButton (string lvl)
	{
		Application.LoadLevel (lvl);
		
	}


	//Will load scene based on level string set by external gameobject
	public void NextLevelButton ()
	{
		Debug.Log ("level loading");
		if (level != null)
			Application.LoadLevel (level);
	}
}
