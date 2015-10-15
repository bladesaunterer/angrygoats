using UnityEngine;
using System.Collections;

public class GemSwitching : MonoBehaviour
{

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;
	private GemManager gemManager = GemManager.Instance;


	void Awake ()
	{
		Debug.Log (gemManager.GetGemOne ().ToString () + " successfully persisted");
		Debug.Log (gemManager.GetGemTwo ().ToString () + " successfully persisted");

		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		gemOne = GameObject.FindGameObjectWithTag (gemManager.GetGemOne ().ToString ());
		gemTwo = GameObject.FindGameObjectWithTag (gemManager.GetGemTwo ().ToString ());



		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive (false);
		}
		gemOne.SetActive (true);
		
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
		if (gemOne.gameObject.activeSelf) {
			gemOne.SetActive (false);
			gemTwo.SetActive (true);
			gemManager.SetCurrentGem (gemManager.GetGemTwo ());
		} else {
			gemOne.SetActive (true);
			gemTwo.SetActive (false);
			gemManager.SetCurrentGem (gemManager.GetGemOne ());
		}
	}

//	public Gem GetCurrentGem ()
//	{
//		return currentGem;
//	}
}
