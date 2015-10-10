using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneTextScript : MonoBehaviour {
	
	public Text textBoxString;
	public TextAsset TextFile;
	public float textTypingDelay;

	private string[] linesInFile;
	private string[] scriptLine;
	private int lineNumber;

	void Start() {
		textBoxString = GameObject.FindGameObjectWithTag("SPreLevDialogueText").GetComponentInChildren<Text>();
		linesInFile = TextFile.text.Split('\n');
		lineNumber = 0;

		scriptLine = linesInFile[lineNumber].Split(':');
		StartCoroutine(TypeOutText(scriptLine[1]));

		lineNumber++;
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (lineNumber < linesInFile.Length) {
				scriptLine = linesInFile[lineNumber].Split(':');
				if (scriptLine[0] == "Ndoto") {
					textBoxString.color = Color.red;
				} else {
					textBoxString.color = Color.black;
				}
				StartCoroutine(TypeOutText(scriptLine[1]));
				lineNumber++;
			}
		}
	}

	IEnumerator TypeOutText(string lineText) {
		textBoxString.text = "";
		foreach (char c in lineText.ToCharArray()) {
			textBoxString.text += c;
			yield return new WaitForSeconds(textTypingDelay);
		}
	}
}