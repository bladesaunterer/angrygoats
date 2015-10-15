using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class highScoreBoard : MonoBehaviour {

	public Text levelOne;
	public Text levelTwo;
	public Text levelThree;


	// Use this for initialization
	void Start () {
		levelOne.text = PlayerPrefs.GetInt ("High Score 1").ToString();
		levelTwo.text = PlayerPrefs.GetInt ("High Score 2").ToString();
		levelThree.text = PlayerPrefs.GetInt ("High Score 3").ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
