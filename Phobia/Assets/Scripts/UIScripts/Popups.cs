using UnityEngine;
using System.Collections;

/**
* Class for displaying the win/death popups when the player wins of dies
**/
public class Popups : MonoBehaviour {

    public GameObject winScreen;
    public GameObject deadScreen;

    // Indicates if there is already a win/death popup displayed
    private bool popupDisplaying = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Only deal with popups if one not already displaying
        if (popupDisplaying == false)
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

    // Displays the win screen and updates the score on it
    void displayWinScreen()
    {
        popupDisplaying = true;
        winScreen.SetActive(popupDisplaying);
        int temp1 = TEMPScoreScript.Instance.GetScore();
        int temp2 = TEMPScoreScript.Instance.GetEnemies();
        GameObject time = GameObject.Find("Timer");
        int temp3 = time.GetComponent<Timer>().getMinutes();
        int temp4 = time.GetComponent<Timer>().getSeconds();
        winScreen.GetComponent<WinUpdate>().SetFinal(temp1, temp2, temp3, temp4);
    }

    // Display the death screen
    void displayDeathScreen()
    {
        popupDisplaying = true;
        deadScreen.SetActive(popupDisplaying);
    }
}
