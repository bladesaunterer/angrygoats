using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneTextScript : MonoBehaviour {
	
	public Text textBoxString;

	void Start() {

		textBoxString = GameObject.FindGameObjectWithTag("SPreLevDialogueText").GetComponentInChildren<Text>();
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");
			textBoxString.text = "PressedLeft.";

		}
	}
}