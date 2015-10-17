using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highScoreBoard : MonoBehaviour {

	public Text levelOne;
	public Text levelTwo;
	public Text levelThree;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("SpiderLevelScene name") == "") {
			levelOne.text = "None yet!";
		} else {
			levelOne.text = PlayerPrefs.GetString("SpiderLevelScene name") + ": " + PlayerPrefs.GetInt ("SpiderLevelScene").ToString();
		}

		if (PlayerPrefs.GetString ("HeightsLevelScene name") == "") {
			levelTwo.text = "None yet!";
		} else {
			levelTwo.text = PlayerPrefs.GetString("HeightsLevelScene name") + ": " + PlayerPrefs.GetInt ("HeightsLevelScene").ToString();
		}

		if (PlayerPrefs.GetString ("DarknessLevelScene name") == "") {
			levelThree.text = "None yet!";
		} else {
			levelThree.text = PlayerPrefs.GetString("DarknessLevelScene name") + ": " + PlayerPrefs.GetInt ("DarknessLevelScene").ToString();
		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
