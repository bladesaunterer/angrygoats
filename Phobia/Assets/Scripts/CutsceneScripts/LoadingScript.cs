using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	public string levelToTransitionTo;

	// Use this for initialization
	void Start () {
		StartCoroutine ("TransitionToGame");
	}
	
	/**
	 *
	 * Coroutine Method for loading a game 
	 * level asyncronously.
	 *
	 **/
	IEnumerator TransitionToGame()
	{
		AsyncOperation async = Application.LoadLevelAsync(levelToTransitionTo);
		while (!async.isDone)
		{
			yield return(0);
		}
	}
}
