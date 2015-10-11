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
        
		public void NotifyToggleOn (GemToggle toggle)
		{
			ValidateToggleIsInGroup (toggle);
            
			if (allowMultipleSelection)
				return;
            
			// disable all toggles in the group
			for (var i = 0; i < m_Toggles.Count; i++) {

				if (m_Toggles [i] == toggle || m_Toggles [i].LastGemSelected) {
					continue;
				}

				m_Toggles [i].isOn = false;
                
			}

			for (var i = 0; i < m_Toggles.Count; i++) {
				
				if (m_Toggles [i].LastGemSelected) {
					m_Toggles [i].LastGemSelected = false;
				}

				if (m_Toggles [i] == toggle) {
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