using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/**
 * Script attatched to name field to register name with player prefs
 */
public class EnterNameScript : MonoBehaviour
{

	public InputField NameField;
	public Button EnterButton;

	public void Enter ()
	{
		PlayerPrefs.SetString (Application.loadedLevelName + " name", NameField.text);
		gameObject.SetActive (false);
	}

	// Use this for initialization
	void Start ()
	{
		EnterButton.onClick.AddListener (() => Enter ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			Enter ();
		}
	}
}
