using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highScoreBoard : MonoBehaviour {

	public Text levelOne;
	public Text levelTwo;
	public Text levelThree;


	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetString ("SpiderLevelScene 1 name") == "") {
			levelOne.text = "None yet!";
		} else {
			levelOne.text = PlayerPrefs.GetString("SpiderLevelScene 1 name") + ": " + PlayerPrefs.GetInt ("SpiderLevelScene 1").ToString();
		}

		if (PlayerPrefs.GetString ("High Score 2 name") == "") {
			levelTwo.text = "None yet!";
		} else {
			levelTwo.text = PlayerPrefs.GetString("High Score 2 name") + ": " + PlayerPrefs.GetInt ("High Score 2").ToString();
		}

		if (PlayerPrefs.GetString ("High Score 3 name") == "") {
			levelThree.text = "None yet!";
		} else {
			levelThree.text = PlayerPrefs.GetString("High Score 3 name") + ": " + PlayerPrefs.GetInt ("High Score 3").ToString();
		}



	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
