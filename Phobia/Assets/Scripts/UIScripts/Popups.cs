using UnityEngine;
using System.Collections;

/**
* Class for displaying the win/death popups when the player wins of dies
**/
public class Popups : MonoBehaviour {

	public GameObject pauseScreen;
    public GameObject winScreen;
    public GameObject deadScreen;

    // Indicates if there is already a win/death popup displayed
    private bool popupDisplaying = false;
	private GameObject minimapObject;
	
		// Use this for initialization
	void Start () {
	
		Time.timeScale = 1.0f;
		minimapObject = GameObject.Find("Minimap");
	}
	
	// Update is called once per frame
	void Update () {
		if (popupDisplaying && Input.GetKeyDown(KeyCode.LeftShift)) 
		{
			minimapObject.GetComponent<MinimapScript>().enabled = false;
		}
		else if (Input.GetKeyDown(KeyCode.Escape)) 
		{
			// Game Pausing Logic here
			togglePauseScreen();
		} 

        // Only deal with popups if one not already displaying
		else if (popupDisplaying == false)
        {
            // if boss destroyed display win screen
            if (GameObject.FindGameObjectWithTag("Boss") == null)
            {
                displayWinScreen();
            }
            // if player destroyed display death screen
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                displayDeathScreen();
            }
        }
	}

	// Displays the pause screen
	public void togglePauseScreen() {
		// Toggle to false or true accordingly.
		if (popupDisplaying == false) {
			popupDisplaying = true;

			Time.timeScale = 0.0f;
			minimapObject.GetComponent<MinimapScript>().enabled = false;
		} else {
			popupDisplaying = false;

			Time.timeScale = 1.0f;
			minimapObject.GetComponent<MinimapScript>().enabled = true;
		}

		// Set pause screen visibility according to boolean.
		pauseScreen.SetActive (popupDisplaying);
	}

    // Displays the win screen and updates the score on it
    void displayWinScreen()
    {
        popupDisplaying = true;
		Time.timeScale = 0.0f;
		int temp1 = TEMPScoreScript.Instance.GetScore();
        int temp2 = TEMPScoreScript.Instance.GetEnemies();
        GameObject time = GameObject.Find("Timer");
        int temp3 = time.GetComponent<Timer>().getMinutes();
        int temp4 = time.GetComponent<Timer>().getSeconds();
        winScreen.GetComponent<WinUpdate>().SetFinal(temp1, temp2, temp3, temp4);
        winScreen.SetActive(popupDisplaying);
    }

    // Display the death screen
    void displayDeathScreen()
    {
        popupDisplaying = true;
		Time.timeScale = 0.0f;
		deadScreen.SetActive(popupDisplaying);
    }
}
