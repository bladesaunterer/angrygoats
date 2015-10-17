using UnityEngine;
using System.Collections;


/**
 * singleton which handles all information regarding gems. Can be used universally to access
 * and set information about gems and will persist information between 
 * scenes and games
 */
public class GemManager
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

	//will set the default gems to blue. For when the values dont get overidden
	private Gem defaultGemOne = Gem.Blue;
	private Gem defaultGemTwo = Gem.Blue;

	/**
	 * Will register default gem one
	 */
	public void SetDefaultGemOne (Gem dGemOne)
	{
		PlayerPrefs.SetString ("DefaultGemOne", dGemOne.ToString ());
	}

	/**
	 * Will register default gem two
	 */
	public void SetDefaultGemTwo (Gem dGemTwo)
	{
		PlayerPrefs.SetString ("DefaultGemTwo", dGemTwo.ToString ());
	}

	/**
	 * Will return value of default gem one
	 */
	public Gem GetDefaultGemOne ()
	{
		string gem = PlayerPrefs.GetString ("DefaultGemOne");
		return GetEnum (gem);
	}

	/**
	 * Will return value of default gem two
	 */
	public Gem GetDefaultGemTwo ()
	{
		string gem = PlayerPrefs.GetString ("DefaultGemTwo");
		return GetEnum (gem);
	}

	/**
	 * Registers Selected Gem one (non default)
	 */

	public void SetGemOne (Gem gemOne)
	{
		PlayerPrefs.SetString ("GemOne", gemOne.ToString ());
	}

	/**
	 * Registers Selected Gem two (non default)
	 */
	public void SetGemTwo (Gem gemTwo)
	{
		PlayerPrefs.SetString ("GemTwo", gemTwo.ToString ());
	}

	/**
	 * Returns Selected Gem one (non default)
	 */
	public Gem GetGemOne ()
	{
		string gem = PlayerPrefs.GetString ("GemOne");
		return GetEnum (gem);
	}

	/**
	 * Returns Selected Gem two (non default)
	 */
	public Gem GetGemTwo ()
	{
		string gem = PlayerPrefs.GetString ("GemTwo");
		return GetEnum (gem);
	}

	/**
	 * Will set the value of the gem currently being used by the player
	 */
	public void SetCurrentGem (Gem gem)
	{
		PlayerPrefs.SetString ("CurrentGem", gem.ToString ());

	}

	/**
	 * Will return the value of the gem currently being used by the player
	 */
	public Gem GetCurrentGem ()
	{
		string gem = PlayerPrefs.GetString ("CurrentGem");
		return GetEnum (gem);

	}

	/**
	 * Will register a gem as being unlocked.
	 */
	public void UnlockGem (Gem gem)
	{
		PlayerPrefs.SetString (gem.ToString (), "unlocked");
	}

	/**
	 * Will register a gem as being locked
	 */
	public void LockGem (Gem gem)
	{
		PlayerPrefs.SetString (gem.ToString (), "locked");
	}

	/**
	 * Will register all gems as unlocked
	 */
	public void UnlockAllGems ()
	{
		foreach (Gem g in Gem.GetValues(typeof(Gem))) {
			if (g != Gem.None)
				UnlockGem (g);
		}
	}


	/**
	 * Will register all gems as locked
	 */
	public void LockAllGems ()
	{
		foreach (Gem g in Gem.GetValues(typeof(Gem))) {
			if (g != Gem.None)
				LockGem (g);
		}

	}

	/**
	 * Will set gem unlocking state if its the players first game
	 */
	public void CheckFirstGame ()
	{
		if (!PlayerPrefs.HasKey (Gem.Blue.ToString ()))
			LockAllGems ();

	}

	/**
	 * Checks if a gem has been unlocked
	 */
	public bool CheckIfGemUnlocked (Gem gem)
	{
		if (PlayerPrefs.HasKey (gem.ToString ()) && PlayerPrefs.GetString (gem.ToString ()).Equals ("unlocked")) {
			return true;
		} else {
			return false;
		}
	}

	/**
	 * Will reset players selected gems to be the default ones
	 */
	public void ResetToDefaultSelection ()
	{
		SetGemOne (GetDefaultGemOne ());
		SetGemTwo (GetDefaultGemTwo ());

	}

	/**
	 * Will register default gems
	 */
	public void SetDefaultSelection (Gem one, Gem two)
	{
		SetDefaultGemOne (one);
		SetDefaultGemTwo (two);
		ResetToDefaultSelection ();
	}

	/**
	 * Will reset selection to default 
	 */
	public void ClearGemOne ()
	{
		SetGemOne (GetDefaultGemOne ());
	}

	/**
	 * Will reset selection to default 
	 */
	public void ClearGemTwo ()
	{
		SetGemOne (GetDefaultGemOne ());
	}
	                       

	public Gem GetEnum (string gem)
	{

		foreach (Gem g in Gem.GetValues(typeof(Gem))) {
			if (g.ToString ().Equals (gem))
				return g;
		}
		return Gem.None;

	}



}
