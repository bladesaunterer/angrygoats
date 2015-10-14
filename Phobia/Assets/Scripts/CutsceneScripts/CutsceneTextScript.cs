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
	public Text nameBoxString;
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
		if (Input.GetKeyDown (KeyCode.Escape)) {
		
			//Skip cutscene to game loading scene.
			Application.LoadLevelAsync("LoadingScene");

		} else if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space")) {

			CutsceneSoundScript.PlayNextTextBoxSound();

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
				//Transition to game loading scene.
				Application.LoadLevelAsync("LoadingScene");
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

		// Set name of speaker.
		nameBoxString.text = scriptLine [0];

		// Determine what image to highlight.
		if (scriptLine [0] == "Dr. Ndoto") {
			portraitLImage.color = Color.white;
			portraitRImage.color = Color.gray;

		} else if (scriptLine [0] == "Client") {
			portraitLImage.color = Color.gray;
			portraitRImage.color = Color.white;

		} else {
			portraitLImage.color = Color.gray;
			portraitRImage.color = Color.gray;
		}

		// Get dialogue string to write to text box. 
		lineText = scriptLine[1];

		// Print out string character by character.
		foreach (char c in lineText.ToCharArray()) {
			textBoxString.text += c;
			CutsceneSoundScript.PlayTextSound();
			yield return new WaitForSeconds(textTypingDelay);
		}
	}
}