using UnityEngine;
using System.Collections;


public class GemSelection : MonoBehaviour
{
	public Gem gemOne;
	public Gem gemTwo;

	void Awake ()
	{
		DontDestroyOnLoad (this);
	}
	// Use this for initialization
	public void selectGems (Gem gemOne, Gem gemTwo)
	{
		this.gemOne = gemOne;
		this.gemTwo = gemTwo;
	}

}
