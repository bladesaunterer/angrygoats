using UnityEngine;
using System.Collections;

public class GemSwitching : MonoBehaviour
{

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;
	private GemSelection GemSelection = new GemSelection ();


	void Awake ()
	{
		GemSelection.selectGems (Gem.Red, Gem.Green);
//		PlayerPrefs.SetString (Gem.GemOne.ToString (), Gem.Yellow.ToString ());
//		PlayerPrefs.SetString (Gem.GemTwo.ToString (), Gem.Blue.ToString ());
	
		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		//Debug.Log (gem.gemOne.ToString ());
		gemOne = GameObject.FindGameObjectWithTag (PlayerPrefs.GetString ("GemOne"));
		gemTwo = GameObject.FindGameObjectWithTag (PlayerPrefs.GetString ("GemTwo"));
		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive (false);
		}
		gemOne.SetActive (true);
		
		//current selection starts with gemOne
		currentGem = GemSelection.GetGemOne ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.L)) {
			ChangeGem ();
		}
	}

	void ChangeGem ()
	{
		if (gemOne.gameObject.activeSelf) {
			gemOne.SetActive (false);
			gemTwo.SetActive (true);
			currentGem = GemSelection.GetGemTwo ();
		} else {
			gemOne.SetActive (true);
			gemTwo.SetActive (false);
			currentGem = GemSelection.GetGemTwo ();
		}
	}

//	public Gem GetCurrentGem ()
//	{
//		return currentGem;
//	}
}
