﻿using UnityEngine;
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
		
		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		gemOne = GameObject.FindGameObjectWithTag (GemSelection.GetGemOne ().ToString ());
		gemTwo = GameObject.FindGameObjectWithTag (GemSelection.GetGemTwo ().ToString ());

		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive (false);
		}
		gemOne.SetActive (true);
		gemTwo.SetActive(true);
		gemTwo.GetComponent<MeshRenderer>().enabled = false;
		
		//current selection starts with gemOne
		GemSelection.SetCurrentGem (GemSelection.GetGemOne ());

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
		if (gemOne.gameObject.GetComponent<MeshRenderer>().enabled) {
			gemOne.GetComponent<MeshRenderer>().enabled = false;
			gemTwo.GetComponent<MeshRenderer>().enabled = true;
			//gemOne.SetActive (false);
			//gemTwo.SetActive (true);
			GemSelection.SetCurrentGem (GemSelection.GetGemTwo ());
		} else {
			gemOne.GetComponent<MeshRenderer>().enabled = true;
			gemTwo.GetComponent<MeshRenderer>().enabled = false;
			//gemOne.SetActive (true);
			//gemTwo.SetActive (false);
			GemSelection.SetCurrentGem (GemSelection.GetGemOne ());
		}
	}

//	public Gem GetCurrentGem ()
//	{
//		return currentGem;
//	}
}
