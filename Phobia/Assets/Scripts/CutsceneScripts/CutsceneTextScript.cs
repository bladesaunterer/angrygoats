using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneTextScript : MonoBehaviour {
	
	public Text textBoxString;
	public TextAsset TextFile;
	public float textTypingDelay;

	private string[] linesInFile;
	private string[] scriptLine;
	private string lineText;
	private int lineNumber;

	void Start() {
		textBoxString = GameObject.FindGameObjectWithTag("SPreLevDialogueText").GetComponentInChildren<Text>();
		linesInFile = TextFile.text.Split('\n');
		lineNumber = 0;

		scriptLine = linesInFile[lineNumber].Split(':');
		lineText = scriptLine[1];
		StartCoroutine("TypeOutText");

		lineNumber++;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (lineNumber < linesInFile.Length) {
				StopCoroutine("TypeOutText");
				scriptLine = linesInFile[lineNumber].Split(':');
				if (scriptLine[0] == "Ndoto") {
					textBoxString.color = Color.red;
				} else {
					textBoxString.color = Color.black;
				}
				lineText = scriptLine[1];
				StartCoroutine("TypeOutText");
				lineNumber++;
			}
		}
	}

	IEnumerator TypeOutText() {
		textBoxString.text = "";
		foreach (char c in lineText.ToCharArray()) {
			textBoxString.text += c;
			yield return new WaitForSeconds(textTypingDelay);
		}
	}
}