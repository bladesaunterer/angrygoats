using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneTextScript : MonoBehaviour {
	
	public Text textBoxString;
	public TextAsset TextFile;
	public Image portraitLImage;
	public Image portraitRImage;
	public float textTypingDelay;

	private string[] linesInFile;
	private string[] scriptLine;
	private string lineText;
	private int lineNumber;

	void Start() {
		textBoxString = GameObject.FindGameObjectWithTag("SPreLevDialogueText").GetComponentInChildren<Text>();
		//portraitLImage = GameObject.FindGameObjectWithTag("portraitLImage").GetComponentInChildren<Image>();
		//portraitRImage = GameObject.FindGameObjectWithTag("portraitRImage").GetComponentInChildren<Image>();

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
				// Stop current Coroutine for typing.
				StopCoroutine("TypeOutText");
				scriptLine = linesInFile[lineNumber].Split(':');
				StartCoroutine("TypeOutText");
				lineNumber++;
			}
		}
	}

	IEnumerator TypeOutText() {
		textBoxString.text = "";

		if (scriptLine[0] == "Ndoto") {
			textBoxString.color = Color.red;
			portraitLImage.color = Color.white;
			portraitRImage.color = Color.gray;
		} else if (scriptLine[0] == "Client"){
			textBoxString.color = Color.black;
			portraitLImage.color = Color.gray;
			portraitRImage.color = Color.white;
		}
		lineText = scriptLine[1];

		foreach (char c in lineText.ToCharArray()) {
			textBoxString.text += c;
			yield return new WaitForSeconds(textTypingDelay);
		}
	}
}