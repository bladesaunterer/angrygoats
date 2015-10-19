using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	/**
	 * Custom Toggle grouping script for gem selection. Will restrict selection to any two 
	 * active toggles in group. Uses gem manager to restrict selection to unlocked gems
	 * Author: Carson, Zoe
	 * 
	 * based on scripts from:
	 * http://answers.unity3d.com/questions/908591/is-there-a-multiple-selection-ugui-toggle-group.html
	 */
	[AddComponentMenu("UI/Toggle Group Custom", 36)]
	public class GemToggleGroup : UIBehaviour
	{
		public Gem GemOneDefault;
		public Gem GemTwoDefault;

		//boolean determining whehter or not at least one toggle needs to be on
		[SerializeField]
		private bool
			m_AllowSwitchOff = false;
		public bool allowSwitchOff { get { return m_AllowSwitchOff; } set { m_AllowSwitchOff = value; } }
        

        
		private List<GemToggle> m_Toggles = new List<GemToggle> ();
		private GemManager gm = GemManager.Instance;
        
		protected GemToggleGroup ()
		{
		}

		/**
		 * Will run everytime toggle in group is clicked. 
		 */
		public void NotifyToggleClick (GemToggle toggle)
		{
	

			if (ActiveToggles ().Count () == 1) {
				for (var i = 0; i < m_Toggles.Count; i++) {
					if (m_Toggles [i].isOn) {
						gm.SetGemOne (m_Toggles [i].AssociatedGem);
						gm.SetGemTwo (m_Toggles [i].AssociatedGem);
					}	
				}
			}
			if (!toggle.isOn) {

				if (gm.GetGemOne () == toggle.AssociatedGem)
					gm.ClearGemOne ();
				else if (gm.GetGemTwo () == toggle.AssociatedGem)
					gm.ClearGemTwo ();
			}
			Debug.Log ("***********************");
			Debug.Log (gm.GetGemOne ().ToString ());
			Debug.Log (gm.GetGemTwo ().ToString ());

			
		}
        
		/**
		 * Called whenever a toggle is turned on. Will determine which 
		 * toggles get turned off
		 */
		public void NotifyToggleOn (GemToggle toggle)
		{  
			//will disable all toggles expcept current toggle and last selected toggle
			for (var i = 0; i < m_Toggles.Count; i++) {

				if (m_Toggles [i] == toggle || m_Toggles [i].LastGemSelected) {
					continue;
				} 

				m_Toggles [i].isOn = false;
                
			}

			//Will change booleans associated with toggles to match current game state
			for (var i = 0; i < m_Toggles.Count; i++) {
				if (m_Toggles [i].LastGemSelected) {
					gm.SetGemTwo (m_Toggles [i].AssociatedGem);
					m_Toggles [i].LastGemSelected = false;
				}

				if (m_Toggles [i] == toggle) {
					gm.SetGemOne (m_Toggles [i].AssociatedGem);
					m_Toggles [i].LastGemSelected = true;
				}

			}

		}

		/**
		 * Removes toggle from group
		 */
		public void UnregisterToggle (GemToggle toggle)
		{
			if (m_Toggles.Contains (toggle))
				m_Toggles.Remove (toggle);
		}
        

		/**
		 * Cheat Mode, unlocks everything on Key press C, resets to normal
		 * on key press B
		 */
		public void Update ()
		{
			if (Input.GetKeyDown (KeyCode.B)) {
				PlayerPrefs.SetInt ("SpiderLevelScene", 0);
				PlayerPrefs.SetString ("SpiderLevelScene name", "");
				PlayerPrefs.SetInt ("HeightsLevelScene", 0);
				PlayerPrefs.SetString ("HeightsLevelScene name", "");
				PlayerPrefs.SetInt ("DarknessLevelScene", 0);
				PlayerPrefs.SetString ("DarknessLevelScene name", "");
				LevelManager.Instance.resetLevels ();
				LevelManager.Instance.firstGameSetup (Level.Spider);
			}
			if (Input.GetKeyDown (KeyCode.C)) {
				PlayerPrefs.SetInt ("SpiderLevelScene", 700);
				PlayerPrefs.SetString ("SpiderLevelScene name", "zoe");
				PlayerPrefs.SetInt ("HeightsLevelScene", 700);
				PlayerPrefs.SetString ("HeightsLevelScene name", "zoe");
				PlayerPrefs.SetInt ("DarknessLevelScene", 700);
				PlayerPrefs.SetString ("DarknessLevelScene name", "zoe");	
				LevelManager.Instance.unlockAllLevels ();
			}

		}


		/**
		 * Adds toggle to group
		 */
		public void RegisterToggle (GemToggle toggle)
		{
			//following line used for testing
			//gm.LockAllGems ();


			// Will set  up gem system if hasnt been used before
			gm.CheckFirstGame ();

			//will unlock the default gems
			gm.UnlockGem (GemOneDefault);
			gm.UnlockGem (GemTwoDefault);

			if (PlayerPrefs.GetInt ("SpiderLevelScene") > 0) {
				gm.UnlockGem (Gem.Blue);
			}

			if (PlayerPrefs.GetInt ("HeightsLevelScene") > 0) {
				gm.UnlockGem (Gem.Turquoise);
			}

			if (PlayerPrefs.GetInt ("DarknessLevelScene") > 0) {
				gm.UnlockGem (Gem.Yellow);
			}

			if (PlayerPrefs.GetInt ("SpiderLevelScene") > 500 && 
				PlayerPrefs.GetInt ("HeightsLevelScene") > 500 && 
				PlayerPrefs.GetInt ("DarknessLevelScene") > 500) {
				gm.UnlockGem (Gem.Purple);
			}


			//will register the default selection to gem manager
			gm.SetDefaultSelection (GemOneDefault, GemTwoDefault);

			Debug.Log (gm.GetDefaultGemOne ().ToString () + "  default gem 1");
			Debug.Log (gm.GetDefaultGemTwo ().ToString () + "  default gem 2");

			toggle.isOn = false;
			toggle.LastGemSelected = false;

			//Will set gem toggle state for default gems
			if (toggle.AssociatedGem == gm.GetDefaultGemOne ()) {
				toggle.isOn = true;
				toggle.LastGemSelected = false;
			} else if (toggle.AssociatedGem == gm.GetDefaultGemTwo ()) {
				toggle.isOn = true;
				toggle.LastGemSelected = true;
			}

			if (!m_Toggles.Contains (toggle))
				m_Toggles.Add (toggle);

		}
        
		/**
		 * Whether any toggles are on
		 */
		public bool AnyTogglesOn ()
		{
			return m_Toggles.Find (x => x.isOn) != null;
		}
        
		/*
		 * Returns list of active toggles
		 */
		public IEnumerable<GemToggle> ActiveToggles ()
		{
			return m_Toggles.Where (x => x.isOn);
		}
        
		/*
		 * Will turn off all toggles
		 */
		public void SetAllTogglesOff ()
		{
			bool oldAllowSwitchOff = m_AllowSwitchOff;
			m_AllowSwitchOff = true;
            
			for (var i = 0; i < m_Toggles.Count; i++)
				m_Toggles [i].isOn = false;
            
			m_AllowSwitchOff = oldAllowSwitchOff;
		}
	}
}