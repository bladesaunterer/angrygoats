using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

/**
 * Custom toggle elements for gems. State of toggle is controlled here and is set based 
 * on whether or not its associated gem is unlocked. If gem is locked, the toggle
 * will be set to be non interactable
 * 
 * http://answers.unity3d.com/questions/908591/is-there-a-multiple-selection-ugui-toggle-group.html
 */


namespace UnityEngine.UI
{
	/**
 	* Custom toggle elements for gems. State of toggle is controlled here and is set based 
 	* on whether or not its associated gem is unlocked. If gem is locked, the toggle
 	* will be set to be non interactable
 	*
 	* based on code from:
 	* http://answers.unity3d.com/questions/908591/is-there-a-multiple-selection-ugui-toggle-group.html
 	*/
	[AddComponentMenu("UI/Toggle Custom", 35)]
	[RequireComponent(typeof(RectTransform))]
	public class GemToggle : Selectable, IPointerClickHandler, ISubmitHandler, ICanvasElement
	{

		//Boolean used in toggle group to control whether or not toggle should stay on
		public bool LastGemSelected = false;

		//Gem associated with toggle
		public Gem AssociatedGem;


		public enum ToggleTransition
		{
			None,
			Fade
		}
		
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}
		
		// Whether the toggle is on
		[FormerlySerializedAs("m_IsActive")]
		[Tooltip("Is the toggle currently on or off?")]
		[SerializeField]
		private bool
			m_IsOn;
		
		/**
		 * Transition Type
		 */
		public ToggleTransition toggleTransition = ToggleTransition.Fade;
		

		/**
		 * Graphich that the toggle works with
		 */
		public Graphic checkGraphic;

		/**
		 * Needs to be gameobject with image script attached in order to use 
		 * SetActive method
		 */
		public GameObject lockedGraphic;

	
		
		
		
		// group that this toggle can belong to
		[SerializeField]
		private GemToggleGroup
			m_Group;
		
		public GemToggleGroup group {
			get { return m_Group; }
			set {
				m_Group = value;
				#if UNITY_EDITOR
				if (Application.isPlaying) {
					#endif
					SetToggleGroup (m_Group, true);
					PlayEffect (true);
				}
			}
		}
		
		/**
		 * Allow for delegate-based subscriptions for faster events than 'eventReceiver', and allowing for multiple receivers.
		 * Used by other objects to check state of toggle
		 */
		public ToggleEvent onValueChanged = new ToggleEvent ();
		
		protected GemToggle ()
		{
			
		}
		
		#if UNITY_EDITOR
		protected override void OnValidate ()
		{
			base.OnValidate ();
			Set (m_IsOn, false);
			PlayEffect (toggleTransition == ToggleTransition.None);
			
			var prefabType = UnityEditor.PrefabUtility.GetPrefabType (this);
			if (prefabType != UnityEditor.PrefabType.Prefab && !Application.isPlaying)
				CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild (this);
		}
		
		#endif // if UNITY_EDITOR
		
		public virtual void Rebuild (CanvasUpdate executing)
		{
			#if UNITY_EDITOR
			if (executing == CanvasUpdate.Prelayout)
				onValueChanged.Invoke (m_IsOn);
			#endif
		}
		
		protected override void OnEnable ()
		{
			base.OnEnable ();
			SetToggleGroup (m_Group, false);
			PlayEffect (true);
		}
		
		protected override void OnDisable ()
		{
			SetToggleGroup (null, false);
			base.OnDisable ();
		}
		
		private void SetToggleGroup (GemToggleGroup newGroup, bool setMemberValue)
		{
			GemToggleGroup oldGroup = m_Group;     

			// Sometimes IsActive returns false in OnDisable so don't check for it.
			// Rather remove the toggle too oftem than too little.
			if (m_Group != null)
				m_Group.UnregisterToggle (this);
			
			// At runtime the group variable should be set but not when calling this method from OnEnable or OnDisable.
			// That's why we use the setMemberValue parameter.
			if (setMemberValue)
				m_Group = newGroup;
			
			// Only register to the new group if this Toggle is active.
			if (m_Group != null && IsActive ())
				m_Group.RegisterToggle (this);
			
			// If we are in a new group, and this toggle is on, notify group.
			// Note: Don't refer to m_Group here as it's not guaranteed to have been set.
			if (newGroup != null && newGroup != oldGroup && isOn && IsActive ())
				m_Group.NotifyToggleOn (this);
			
			
		}
		
		/*
		 * Whether the toggle is currently active.
		 */
		public bool isOn {
			get { return m_IsOn; }
			set {
				Set (value);
			}
		}
		
		void Set (bool value)
		{
			Set (value, true);
		}
		
		void Set (bool value, bool sendCallback)
		{
			if (m_IsOn == value)
				return;
			
			// if we are in a group and set to true, do group logic
			m_IsOn = value;
			if (m_Group != null && IsActive ()) {
				if (m_IsOn || (!m_Group.AnyTogglesOn () && !m_Group.allowSwitchOff)) {
					m_IsOn = true;
					m_Group.NotifyToggleOn (this);
				}
			}
			
			// Always send event when toggle is clicked, even if value didn't change
			// due to already active toggle in a toggle group being clicked.
			// Controls like SelectionList rely on this.
			// It's up to the user to ignore a selection being set to the same value it already was, if desired.
			PlayEffect (toggleTransition == ToggleTransition.None);
			if (sendCallback)
				onValueChanged.Invoke (m_IsOn);
		}
		
		/**
		 * Play the appropriate effect
		 */
		private void PlayEffect (bool instant)
		{
			if (checkGraphic == null)
				return;

			RenderImage (instant);
		}


		/***
		 * Will set the toggle to be non-interactable and will 
		 * display the locked image
		 * 
		 */
		private void LockToggle ()
		{
			this.isOn = false;
			lockedGraphic.SetActive (true);
			RenderImage (true);
			this.interactable = false;
		}

		/**
		 * Will render the toggle depending on state of isOn boolean
		 */
		private void RenderImage (bool instant)
		{
			#if UNITY_EDITOR
			if (!Application.isPlaying)
				checkGraphic.canvasRenderer.SetAlpha (m_IsOn ? 1f : 0f);
			else
				#endif
				checkGraphic.CrossFadeAlpha (m_IsOn ? 1f : 0f, instant ? 0f : 0.1f, true);
		}

		/**
		 * Will put toggle in correct visual state depending on gem unlock status
		 */
		protected override void Start ()
		{
			if (GemManager.Instance.CheckIfGemUnlocked (this.AssociatedGem)) {
				lockedGraphic.SetActive (false);
				this.interactable = true;
				PlayEffect (true);
			} else {
				LockToggle ();
			}
		}

		private void InternalToggle ()
		{
			if (!IsActive () || !IsInteractable ())
				return;
			
			isOn = !isOn;

		}
		
		/**
		 * Called everytime toggle is clicked
		 */
		public virtual void OnPointerClick (PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
				return;
			
			InternalToggle ();
			m_Group.NotifyToggleClick (this);
		}
		
		
		public virtual void OnSubmit (BaseEventData eventData)
		{
			InternalToggle ();
		}
	}
}