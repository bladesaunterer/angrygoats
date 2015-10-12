using UnityEngine;
using System.Collections;


//needs test to see if gems can be reset on restart
public class GemManager// : PersistentSingleton<GemSelection>
{
	private static GemManager instance;

	public static GemManager Instance {
		get {
			if (instance == null) {
				instance = new GemManager ();
			}
			return GemManager.instance;
		}
		
	}




	// Use this for initialization
	public void SetGemOne (Gem gemOne)
	{
		PlayerPrefs.SetString ("GemOne", gemOne.ToString ());
	}

	public void SetGemTwo (Gem gemTwo)
	{
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

	public void UnlockGem (Gem gem)
	{
		PlayerPrefs.SetString (gem.ToString (), "unlocked");
	}

	public bool CheckIfGemUnlocked (Gem gem)
	{
		if (PlayerPrefs.HasKey (gem.ToString ()) && PlayerPrefs.GetString (gem.ToString ()).Equals ("unlocked")) {
			return true;
		} else {
			return false;
		}
	}

	public void ClearGemSelection ()
	{
		PlayerPrefs.DeleteKey ("GemOne");
		PlayerPrefs.DeleteKey ("GemTwo");

	}

	public void ClearGemOne ()
	{
		PlayerPrefs.DeleteKey ("GemOne");
	}

	public void ClearGemTwo ()
	{
		PlayerPrefs.DeleteKey ("GemOne");
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
			return Gem.None;
		}

	}



}
