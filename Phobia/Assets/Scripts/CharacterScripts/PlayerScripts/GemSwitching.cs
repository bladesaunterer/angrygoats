using UnityEngine;
using System.Collections;

public class GemSwitching : MonoBehaviour
{

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;

	public GameObject currentSelectedGem;

	void Awake ()
	{
		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		gemOne = GameObject.FindGameObjectWithTag ("Red");
		gemTwo = GameObject.FindGameObjectWithTag ("Yellow");
		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive(false);
		}
		gemOne.SetActive (true);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.L)) {
			changeGem ();
		}
	}

	void changeGem ()
	{
		if (gemOne.gameObject.activeSelf) {
			gemOne.SetActive (false);
			gemTwo.SetActive (true);
		} else {
			gemOne.SetActive (true);
			gemTwo.SetActive (false);
		}
	}
}
