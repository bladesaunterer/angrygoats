using UnityEngine;
using System.Collections;

public class DisablePauseScript : MonoBehaviour
{

	public void DisablePause (GameObject pausePopUp)
	{
		Time.timeScale = 1.0f;
		pausePopUp.SetActive (false);
	}
}
