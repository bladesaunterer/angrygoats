using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * 
 * Class which handles cutscene functionality.
 * 
 **/
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
		linesInFile = TextFile.text.Split('\n'); 			// Get lines in file.
		lineNumber = 0;										// Initialize line number to 0.

		scriptLine = linesInFile[lineNumber].Split(':');	// Split line in text file into Speaker and dialogue.
		lineText = scriptLine[1];							// Get dialogue text.
		StartCoroutine("TypeOutText");						// Type out first line to text box. 

		lineNumber++;										// Increment line number.
	}

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			if (lineNumber < linesInFile.Length) {
				// Stop current Coroutine for typing.
				StopCoroutine("TypeOutText");

				// Get next line in text file.
				scriptLine = linesInFile[lineNumber].Split(':'); 

				// Start typing new line.
				StartCoroutine("TypeOutText");

				// Increment line number.
				lineNumber++;
			} else {
				//Transition to game level.
			}
		}
	}

	/**
	 *
	 * Coroutine Method for typing out string from 
	 * file into textbox of cutscene.
	 *
	 **/
	IEnumerator TypeOutText() {

		// Clear text box.
		textBoxString.text = "";

		// Determine what text color to use and image to highlight.
		if (scriptLine[0] == "Ndoto") {
			textBoxString.color = Color.red;
			portraitLImage.color = Color.white;
			portraitRImage.color = Color.gray;
		} else if (scriptLine[0] == "Client"){
			textBoxString.color = Color.black;
			portraitLImage.color = Color.gray;
			portraitRImage.color = Color.white;
		}

		// Get dialogue string to write to text box. 
		lineText = scriptLine[1];

		// Print out string character by character.
		foreach (char c in lineText.ToCharArray()) {
			textBoxString.text += c;
			yield return new WaitForSeconds(textTypingDelay);
		}
	}
}