using UnityEngine;
using System.Collections;

/**
 * 
 * Temporary class which shows pop-up windows
 * for the win and death screens. Necessary as 
 * logic hasn't been implemented yet.
 * 
 **/
public class ToggleUI : MonoBehaviour {
	
	public GameObject dead; // Assign in inspector
	private bool isShowingDead = false;
	public GameObject won;
	private bool isShowingWon = false;
	
	void Update() {
		if (Input.GetKeyDown("escape")) {
			isShowingDead = !isShowingDead;
			dead.SetActive(isShowingDead);
		} else if(Input.GetKeyDown("space")) {
			isShowingWon = !isShowingWon;
			won.SetActive(isShowingWon);
		}
	}
}