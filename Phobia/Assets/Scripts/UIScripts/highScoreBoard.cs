using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highScoreBoard : MonoBehaviour {

	public Text levelOne;
	public Text levelTwo;
	public Text levelThree;
	public Text levelOneName;
	public Text levelTwoName;
	public Text levelThreeName;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("SpiderLevelScene name") == "") {
			levelOneName.text = "No one yet!";
			levelOne.text = " - ";
		} else {
			levelOneName.text = PlayerPrefs.GetString("SpiderLevelScene name");
			levelOne.text = PlayerPrefs.GetInt ("SpiderLevelScene").ToString();
		}

		if (PlayerPrefs.GetString ("HeightsLevelScene name") == "") {
			levelTwoName.text = "No one yet!";
			levelTwo.text = " - ";
		} else {
			levelTwoName.text = PlayerPrefs.GetString("HeightsLevelScene name");
			levelTwo.text = PlayerPrefs.GetInt ("HeightsLevelScene").ToString();
		}

		if (PlayerPrefs.GetString ("DarknessLevelScene name") == "") {
			levelThreeName.text = "No one yet!";
			levelThree.text = " - ";
		} else {
			levelThreeName.text = PlayerPrefs.GetString("DarknessLevelScene name");
			levelThree.text = PlayerPrefs.GetInt ("DarknessLevelScene").ToString();
		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
