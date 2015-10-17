using UnityEngine;
using System.Collections;

/**
 * Script for unpausing the game. 
 **/
public class UnpausingScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Time.timeScale = 1.0f;
	}
}
