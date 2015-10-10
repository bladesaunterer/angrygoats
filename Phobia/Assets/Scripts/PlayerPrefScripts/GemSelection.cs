using UnityEngine;
using System.Collections;


//needs test to see if gems can be reset on restart
public class GemSelection// : PersistentSingleton<GemSelection>
{
	private static GemSelection instance;

	public static GemSelection Instance {
		get {
			if (instance == null) {
				instance = new GemSelection ();
			}
			return GemSelection.instance;
		}
		
	}


	// Use this for initialization
	public void selectGems (Gem gemOne, Gem gemTwo)
	{
		PlayerPrefs.SetString ("GemOne", gemOne.ToString ());
		PlayerPrefs.SetString ("GemTwo", gemTwo.ToString ());
	}

	public Gem GetGemOne ()
	{
		string gem = PlayerPrefs.GetString ("GemOne");
		return getEnum (gem);
	}

	public Gem GetGemTwo ()
	{
		string gem = PlayerPrefs.GetString ("GemTwo");
		return getEnum (gem);
	}

	public void SetCurrentGem (Gem gem)
	{
		PlayerPrefs.SetString ("CurrentGem", gem.ToString ());

	}

	public Gem GetCurrentGem ()
	{
		string gem = PlayerPrefs.GetString ("CurrentGem");
		return getEnum (gem);

	}
	                       

	private Gem getEnum (string gem)
	{
		switch (gem) {
		case "Blue":
			return(Gem.Blue);
		case "Green":
			return(Gem.Green);
		case "Purple":
			return(Gem.Purple);
		case "Red":
			return(Gem.Red);
		case "Turquoise":
			return(Gem.Turquoise);
		case "Yellow":
			return(Gem.Yellow);
		default:
			return Gem.Yellow;
		}

	}



}
