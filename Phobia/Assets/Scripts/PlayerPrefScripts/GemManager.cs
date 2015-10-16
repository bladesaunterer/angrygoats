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

	private Gem defaultGemOne = Gem.Blue;
	private Gem defaultGemTwo = Gem.Blue;

	public void SetDefaultGemOne (Gem dGemOne)
	{
		PlayerPrefs.SetString ("DefaultGemOne", dGemOne.ToString ());
	}

	public void SetDefaultGemTwo (Gem dGemTwo)
	{
		PlayerPrefs.SetString ("DefaultGemTwo", dGemTwo.ToString ());
	}

	public Gem GetDefaultGemOne ()
	{
		string gem = PlayerPrefs.GetString ("DefaultGemOne");
		return getEnum (gem);
	}
	
	public Gem GetDefaultGemTwo ()
	{
		string gem = PlayerPrefs.GetString ("DefaultGemTwo");
		return getEnum (gem);
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

	public void LockGem (Gem gem)
	{

		PlayerPrefs.SetString (gem.ToString (), "locked");

	}

	public void UnlockAllGems ()
	{
		UnlockGem (Gem.Blue);
		UnlockGem (Gem.Green);
		UnlockGem (Gem.Purple);
		UnlockGem (Gem.Red);
		UnlockGem (Gem.Turquoise);
		UnlockGem (Gem.Yellow);
	}

	public void LockAllGems ()
	{
		LockGem (Gem.Blue);
		LockGem (Gem.Green);
		LockGem (Gem.Purple);
		LockGem (Gem.Red);
		LockGem (Gem.Turquoise);
		LockGem (Gem.Yellow);

	}

	/**
	 * Will check if keys exist for gem unlock, will set them if not
	 */
	public void CheckFirstGame ()
	{
		if (!PlayerPrefs.HasKey (Gem.Blue.ToString ()))
			LockAllGems ();

	}
	
	public bool CheckIfGemUnlocked (Gem gem)
	{
		if (PlayerPrefs.HasKey (gem.ToString ()) && PlayerPrefs.GetString (gem.ToString ()).Equals ("unlocked")) {
			return true;
		} else {
			return false;
		}
	}

	public void ResetToDefaultSelection ()
	{
		SetGemOne (GetDefaultGemOne ());
		SetGemTwo (GetDefaultGemTwo ());

	}

	public void ResetToDefaultSelection (Gem one, Gem two)
	{
		SetDefaultGemOne (one);
		SetDefaultGemTwo (two);
		ResetToDefaultSelection ();
	}

	public void ClearGemOne ()
	{
		SetGemOne (GetDefaultGemOne ());
	}

	public void ClearGemTwo ()
	{
		SetGemOne (GetDefaultGemOne ());
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
