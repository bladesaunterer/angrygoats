using UnityEngine;
using System.Collections;

/**
 * Script to disable the popup pause screen
 */
public class DisablePauseScript : MonoBehaviour
{

	public void DisablePause (GameObject pausePopUp)
	{
		Time.timeScale = 1.0f;
		pausePopUp.SetActive (false);
	}
}
