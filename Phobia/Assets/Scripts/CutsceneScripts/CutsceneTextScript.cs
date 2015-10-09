using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneTextScript : MonoBehaviour {
	
	public Text textBoxString;
	public TextAsset TextFile;
	private string[] linesInFile;
	private int lineNumber;

	void Start() {
		textBoxString = GameObject.FindGameObjectWithTag("SPreLevDialogueText").GetComponentInChildren<Text>();
		linesInFile = TextFile.text.Split('\n');
		lineNumber = 0;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");

			if (lineNumber < linesInFile.Length) {
				textBoxString.text = linesInFile[lineNumber];
				lineNumber++;
			}
		}
	}
}