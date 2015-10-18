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

	public BoltMover bolt;

	private GameObject spawn;
	private GameObject gemOne;
	private GameObject gemTwo;
	private Gem currentGem;
	private GemManager GemSelection = new GemManager ();
	
	void Awake () {
		if (fire){
			GemSelection.SetGemOne(Gem.Red);
			GemSelection.SetGemTwo(Gem.Green);
		} else if (lightning){
			GemSelection.SetGemOne(Gem.Yellow);
			GemSelection.SetGemTwo(Gem.Green);
		} else if (ice){
			GemSelection.SetGemOne(Gem.Blue);
			GemSelection.SetGemTwo(Gem.Green);
		}
	
		spawn = GameObject.FindGameObjectWithTag ("SpecialAttack");
		gemOne = GameObject.FindGameObjectWithTag (GemSelection.GetGemOne ().ToString ());
		gemTwo = GameObject.FindGameObjectWithTag (GemSelection.GetGemTwo ().ToString ());
		
		foreach (Transform child in spawn.transform) {
			child.gameObject.SetActive (false);
		}
		gemOne.SetActive (true);
		
		//current selection starts with gemOne
		GemSelection.SetCurrentGem (GemSelection.GetGemOne ());
		bolt.SetGemObject(gemOne);
	}
	
}
