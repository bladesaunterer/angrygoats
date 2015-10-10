using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinUpdate : MonoBehaviour {

	public Text dispText;
	public void SetFinal(int score, int enemies, int minutes, int seconds){
		dispText.text = "Grats! You killed " + enemies.ToString() + " enemies in "
			+ minutes.ToString() +" minutes and " + seconds.ToString() + " seconds, netting you a final score of " + score.ToString() + ".";
	}

}
