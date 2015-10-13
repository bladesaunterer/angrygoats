using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("TransitionToGame");
	}
	
	/**
	 *
	 * Coroutine Method for loading game 
	 * asyncronously.
	 *
	 **/
	IEnumerator TransitionToGame()
	{
		AsyncOperation async = Application.LoadLevelAsync("SpiderLevelScene");
		while (!async.isDone)
		{
			yield return(0);
		}
		
		Debug.Log("Loading complete");
	}
}
