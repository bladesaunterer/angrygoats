﻿using UnityEngine;
using System.Collections;

/**
 * Will handle all logic behind gem logic in game, and will
 * notify gem manager of changes
 */
public class GemSwitching : MonoBehaviour
{

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;
	private GemManager gemManager = GemManager.Instance;

	//Called when script is loaded
	void Awake ()
	{
		Debug.Log(gemManager.GetGemOne().ToString() + " successfully persisted");
		Debug.Log(gemManager.GetGemTwo().ToString() + " successfully persisted");

		spawn = GameObject.FindGameObjectWithTag("SpecialAttack");
		
		//This way broke when people added the gem switching in the hud
		//gemOne = GameObject.FindGameObjectsWithTag (gemManager.GetGemOne ().ToString ());
		//gemTwo = GameObject.FindGameObjectsWithTag (gemManager.GetGemTwo ().ToString ());
		
		foreach (Transform childTransform in gameObject.transform.Find("Shot Spawn")) {
			if (childTransform.gameObject.CompareTag(gemManager.GetGemOne().ToString ()))  {
				gemOne = childTransform.gameObject;
			}
			if (childTransform.gameObject.CompareTag(gemManager.GetGemTwo().ToString()))  {
				gemTwo = childTransform.gameObject;
			}
		}


		//will set all gem game objects to be inactive except selected gems
		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive (false);
		}
		gemOne.SetActive (true);
		gemTwo.SetActive (true);
		gemTwo.GetComponent<MeshRenderer> ().enabled = false;
		
		//current selection starts with gemOne
		gemManager.SetCurrentGem (gemManager.GetGemOne ());

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
		if (gemOne.gameObject.GetComponent<MeshRenderer> ().enabled) {
			gemOne.GetComponent<MeshRenderer> ().enabled = false;
			gemTwo.GetComponent<MeshRenderer> ().enabled = true;
			gemManager.SetCurrentGem (gemManager.GetGemTwo ());
		} else {
			gemOne.GetComponent<MeshRenderer> ().enabled = true;
			gemTwo.GetComponent<MeshRenderer> ().enabled = false;
			gemManager.SetCurrentGem (gemManager.GetGemOne ());
		}
	}


}
