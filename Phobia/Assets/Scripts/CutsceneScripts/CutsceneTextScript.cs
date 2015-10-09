using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneTextScript : MonoBehaviour {
	
	public Text textBoxString;
	public TextAsset TextFile;
	private string[] linesInFile;
	private int lineNumber;
	private int y;

	void Start() {
		textBoxString = GameObject.FindGameObjectWithTag("SPreLevDialogueText").GetComponentInChildren<Text>();
		linesInFile = TextFile.text.Split('\n');
		lineNumber = 0;
		textBoxString.text = linesInFile[lineNumber].Split(':')[1];
		lineNumber++;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (lineNumber < linesInFile.Length) {
				string[] scriptLine = linesInFile[lineNumber].Split(':');
				if (scriptLine[0] == "Ndoto") {
					textBoxString.color = Color.red;
				} else {
					textBoxString.color = Color.black;
				}
				TypeOutText(scriptLine[1]);
				lineNumber++;
			}
		}
	}

	private void TypeOutText(string text) {
		textBoxString.text = text;
	}
}