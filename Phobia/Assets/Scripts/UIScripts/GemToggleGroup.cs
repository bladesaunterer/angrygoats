using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	/**
	 * Custom Toggle grouping script for gem selection. Will restrict selection to any two 
	 * active toggles in group. Uses gem manager to restrict selection to unlocked gems
	 * 
	 * based on scripts from:
	 * http://answers.unity3d.com/questions/908591/is-there-a-multiple-selection-ugui-toggle-group.html
	 */
	[AddComponentMenu("UI/Toggle Group Custom", 36)]
	public class GemToggleGroup : UIBehaviour
	{
		public Gem GemOneDefault;
		public Gem GemTwoDefault;

		[SerializeField]
		private bool
			m_AllowSwitchOff = false;
		public bool allowSwitchOff { get { return m_AllowSwitchOff; } set { m_AllowSwitchOff = value; } }
        
		[SerializeField]
		public bool
			allowMultipleSelection = false;
        
		private List<GemToggle> m_Toggles = new List<GemToggle> ();
		private GemManager gm = GemManager.Instance;
        
		protected GemToggleGroup ()
		{
		}

		/**
		 * Will run everytime toggle in group is clicked. For cases when toggle is
		 * deselected
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
        
		public void NotifyToggleOn (GemToggle toggle)
		{

			gm.SetGemOne (toggle.AssociatedGem);
			gm.SetGemTwo (toggle.AssociatedGem);
            
			if (allowMultipleSelection)
				return;
            
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

		//Removes toggle from group
		public void UnregisterToggle (GemToggle toggle)
		{
			if (m_Toggles.Contains (toggle))
				m_Toggles.Remove (toggle);
		}
        

		//Will add Toggle to group
		public void RegisterToggle (GemToggle toggle)
		{


			/**
			 * Will set  up gem system if hasnt been used before
			 */
			gm.CheckFirstGame ();


			gm.UnlockGem (GemOneDefault);
			gm.UnlockGem (GemTwoDefault);

			/**
			 *Use below line to unlock additional gems 
			 */
			//gm.UnlockGem (Gem.Green);


			gm.ResetToDefaultSelection (GemOneDefault, GemTwoDefault);

			Debug.Log (gm.GetDefaultGemOne ().ToString () + "  default gem 1");
			Debug.Log (gm.GetDefaultGemTwo ().ToString () + "  default gem 2");

			toggle.isOn = false;
			toggle.LastGemSelected = false;

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
        
		public bool AnyTogglesOn ()
		{
			return m_Toggles.Find (x => x.isOn) != null;
		}
        
		public IEnumerable<GemToggle> ActiveToggles ()
		{
			return m_Toggles.Where (x => x.isOn);
		}
        
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