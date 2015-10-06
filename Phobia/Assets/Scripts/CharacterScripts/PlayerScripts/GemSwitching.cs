using UnityEngine;
using System.Collections;

public class GemSwitching : MonoBehaviour
{

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;

	void Awake ()
	{

	
		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		gemOne = GameObject.FindGameObjectWithTag (GemSelection.Instance.gemOne.ToString ());
		gemTwo = GameObject.FindGameObjectWithTag (GemSelection.Instance.gemTwo.ToString ());
		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive(false);
		}
		gemOne.SetActive (true);
		
		//current selection starts with gemOne
		currentGem = GemSelection.Instance.gemOne;

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
			currentGem = GemSelection.Instance.gemTwo;
		} else {
			gemOne.SetActive (true);
			gemTwo.SetActive (false);
			currentGem = GemSelection.Instance.gemOne;
		}
	}

	public Gem GetCurrentGem ()
	{
		return currentGem;
	}
}
