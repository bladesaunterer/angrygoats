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
	public void SetLevel (string level)
	{
		Debug.Log ("level set");
		this.level = level;

	}
	
	public void NextLevelButton (int index)
	{
		Application.LoadLevel (index);
	}
	
	public void NextLevelButton (string lvl)
	{
		Application.LoadLevel (lvl);
		
	}
	
	public void NextLevelButton ()
	{
		Debug.Log ("level loading");
		if (level != null)
			Application.LoadLevel (level);
	}
}
