using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles button logic which moves to different scenes.
 * 
 **/
public class ButtonNextLevel : MonoBehaviour
{

	public void NextLevelButton (int index)
	{
		Application.LoadLevel (index);
	}

	public void NextLevelButton (string levelName)
	{
		Application.LoadLevel (levelName);
	}
}
