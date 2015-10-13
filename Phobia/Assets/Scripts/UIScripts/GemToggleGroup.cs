using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.EventSystems;
/**
 * http://answers.unity3d.com/questions/908591/is-there-a-multiple-selection-ugui-toggle-group.html
 */

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Toggle Group Custom", 36)]
	public class GemToggleGroup : UIBehaviour
	{

		[SerializeField]
		private bool
			m_AllowSwitchOff = false;
		public bool allowSwitchOff { get { return m_AllowSwitchOff; } set { m_AllowSwitchOff = value; } }
        
		[SerializeField]
		public bool
			allowMultipleSelection = false;
        
		private List<GemToggle> m_Toggles = new List<GemToggle> ();
        
		protected GemToggleGroup ()
		{
		}
        
		private void ValidateToggleIsInGroup (GemToggle toggle)
		{
			if (toggle == null || !m_Toggles.Contains (toggle))
				throw new ArgumentException (string.Format ("Toggle {0} is not part of ToggleGroup {1}", toggle, this));
		}

		/**
		 * Will run everytime toggle in group is clicked. For cases when toggle is
		 * deselected
		 */
		public void NotifyToggleClick (GemToggle toggle)
		{
			GemManager gm = GemManager.Instance;

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
				//will set both gems to whatever toggle is on

			}
			Debug.Log ("***********************");
			Debug.Log (gm.GetGemOne ().ToString ());
			Debug.Log (gm.GetGemTwo ().ToString ());

			
		}
        
		public void NotifyToggleOn (GemToggle toggle)
		{
			GemManager gm = GemManager.Instance;

			ValidateToggleIsInGroup (toggle);
			gm.ClearGemSelection ();
			gm.SetGemOne (toggle.AssociatedGem);
			gm.SetGemTwo (toggle.AssociatedGem);
            
			if (allowMultipleSelection)
				return;
            
			// disable all toggles in the group
			for (var i = 0; i < m_Toggles.Count; i++) {
				//Will assign both gems to be the same in the case that only one gem is 
				//unlocked

				if (m_Toggles [i] == toggle || m_Toggles [i].LastGemSelected) {
					continue;
				} 

				m_Toggles [i].isOn = false;
                
			}

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
		
		public void UnregisterToggle (GemToggle toggle)
		{
			if (m_Toggles.Contains (toggle))
				m_Toggles.Remove (toggle);
		}
        
		public void RegisterToggle (GemToggle toggle)
		{
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