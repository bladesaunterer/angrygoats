using UnityEngine;
using System.Collections;

public class GemSwitching : MonoBehaviour
{

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;
	GemSelection gem;

	void Awake ()
	{

		gem = new GemSelection ();
		gem.selectGems (Gem.Black, Gem.Blue);
		PlayerPrefs.SetString ("gemOne", Gem.Blue.ToString ());
		PlayerPrefs.SetString ("gemTwo", Gem.Green.ToString ());
	
		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		Debug.Log (gem.gemOne.ToString ());
		gemOne = GameObject.FindGameObjectWithTag (PlayerPrefs.GetString ("gemOne"));
		gemTwo = GameObject.FindGameObjectWithTag (PlayerPrefs.GetString ("gemTwo"));
		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive (false);
		}
		gemOne.SetActive (true);
		
		//current selection starts with gemOne
		currentGem = gem.gemOne;

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
			currentGem = gem.gemTwo;
		} else {
			gemOne.SetActive (true);
			gemTwo.SetActive (false);
			currentGem = gem.gemOne;
		}
	}

	public Gem GetCurrentGem ()
	{
		return currentGem;
	}
}
