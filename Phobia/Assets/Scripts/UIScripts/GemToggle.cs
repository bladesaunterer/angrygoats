using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GemToggle : MonoBehaviour
{

	public Component[] GemToggles;
	private List<GameObject> gemToggles = new List<GameObject> ();

	// Use this for initialization
	void Awake ()
	{
		foreach (Transform child in transform) {
			gemToggles.Add (transform.GetComponent<GameObject> ());
//			Debug.Log (transform.GetComponent<GameObject> ().tag);

		}
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
