using UnityEngine;
using System.Collections;

public class GemSwitching : MonoBehaviour
{

	private GameObject gemOne;
	private GameObject gemTwo;

	public GameObject currentSelectedGem;

	void Awake ()
	{
		gemOne = GameObject.FindGameObjectWithTag ("GemOne");
		gemTwo = GameObject.FindGameObjectWithTag ("GemTwo");
		currentSelectedGem = gemOne;
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
