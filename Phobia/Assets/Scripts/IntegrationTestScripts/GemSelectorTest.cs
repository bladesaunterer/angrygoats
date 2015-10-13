using UnityEngine;
using System.Collections;

/*
 * This allows us to select the gem we are testing 
 * Attach this to the player and remove the gemswitching script
 */
public class GemSelectorTest : MonoBehaviour {

	public bool fire;
	public bool lightning;
	public bool ice;

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;
	//private GemSelection GemSelection = new GemSelection ();
	
	//void Awake () {
	//	if (fire){
	//		GemSelection.selectGems (Gem.Red, Gem.Green);
	//	} else if (lightning){
	//		GemSelection.selectGems (Gem.Yellow, Gem.Green);
	//	} else if (ice){
	//		GemSelection.selectGems (Gem.Blue, Gem.Green);
	//	}
	
	//	spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
	//	gemOne = GameObject.FindGameObjectWithTag (GemSelection.GetGemOne ().ToString ());
	//	gemTwo = GameObject.FindGameObjectWithTag (GemSelection.GetGemTwo ().ToString ());
		
	//	foreach (Transform child in spawn.transform) {
	//		child.gameObject.SetActive (false);
	//	}
	//	gemOne.SetActive (true);
		
	//	//current selection starts with gemOne
	//	GemSelection.SetCurrentGem (GemSelection.GetGemOne ());
		
	//}
	
	
	// Update is called once per frame
	//void Update ()
	//{
	//	if (Input.GetKeyDown (KeyCode.L)) {
	//		ChangeGem ();
	//	}
	//}
	
	//void ChangeGem ()
	//{
	//	if (gemOne.gameObject.activeSelf) {
	//		gemOne.SetActive (false);
	//		gemTwo.SetActive (true);
	//		GemSelection.SetCurrentGem (GemSelection.GetGemTwo ());
	//	} else {
	//		gemOne.SetActive (true);
	//		gemTwo.SetActive (false);
	//		GemSelection.SetCurrentGem (GemSelection.GetGemOne ());
	//	}
	//}
	
}
