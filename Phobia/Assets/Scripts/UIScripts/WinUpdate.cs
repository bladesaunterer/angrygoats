using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinUpdate : MonoBehaviour {

	public Text dispText;
	public void SetFinal(int score, int enemies, int minutes, int seconds){
		string second;
		string minute;
		int bonus;
		int final;
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

		final = bonus + score;

		dispText.text = "Congratulations\nEnemies killed : " + enemies.ToString () + " (+" + score.ToString () + ")" +
			"\nTime spent : " + minute + ":" + second + " (+" + bonus.ToString() + ")" +
			"\n\n Total : \t\t " + final.ToString();
	}

}
