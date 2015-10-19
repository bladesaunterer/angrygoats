using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * 
 * Class for updating win pop-up text.
 * 
 **/
public class WinUpdate : MonoBehaviour {

	public Text enemyValueText;
	public Text timeValueText;

	public Text enemyBonusCalcText;
	public Text timeBonusCalcText;
	public Text totalCalcText;

	public GameObject highScoreNamePanel;

	/**
	 * 
	 * Method for updating win pop-up text.
	 * 
	 **/
	public void SetFinal(int score, int enemies, int minutes, int seconds) {

		string second;
		string minute;
		int bonus;
		int final;

		//Add zeroes to minute or second strings if required.
		if (seconds < 10) {
			second = "0" + seconds.ToString();	
		} else {
			second = seconds.ToString();
		}
		if (minutes < 10) {
			minute = "0" + minutes.ToString();	
		} else {
			minute = minutes.ToString();
		}

		//Determine Time Bonus based on remaining time:
		if (minutes < 1) {
			bonus = 500;
		} else if (minutes < 2) {
			bonus = 400;
		} else if (minutes < 3) {
			bonus = 300;
		} else if (minutes < 4) {
			bonus = 200;
		} else if (minutes < 5) {
			bonus = 100;
		} else {
			bonus = 0;
		}

		//Calculate total score.
		final = bonus + score;

		if (highScoreNamePanel != null) {
			// PlayerPrefs logic here.
			//		PlayerPrefs.SetInt(Application.loadedLevelName, 0);
			//		print (Application.loadedLevelName);
			if (PlayerPrefs.GetInt (Application.loadedLevelName, -1) == -1) {
				PlayerPrefs.SetInt (Application.loadedLevelName, final);
			} else if (PlayerPrefs.GetInt (Application.loadedLevelName) < final) {
				PlayerPrefs.SetInt (Application.loadedLevelName, final);
				highScoreNamePanel.SetActive (true);
			}
		}

		//Update text with their respective values.
		enemyValueText.text = enemies.ToString ();
		timeValueText.text = minute + ":" + second;

		enemyBonusCalcText.text = score.ToString();
		timeBonusCalcText.text = bonus.ToString();
		totalCalcText.text = final.ToString();
	}

}
