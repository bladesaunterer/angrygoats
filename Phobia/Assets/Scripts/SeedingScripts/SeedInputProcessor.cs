using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeedInputProcessor : MonoBehaviour {

	public InputField seedField;
	public Button beginButton;
	
	public void Enter (){
		PlayerPrefs.SetString ("seed", seedField.text);
	}
	
	// Use this for initialization
	void Start () {
		beginButton.onClick.AddListener(() => Enter());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)){
			Enter();
		}
	}
}
