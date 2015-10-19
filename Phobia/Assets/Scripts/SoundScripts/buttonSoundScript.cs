using UnityEngine;
using System.Collections;

public class buttonSoundScript : MonoBehaviour {

	AudioSource audioSource;

	// Use this for initialization
	void Start () {
		
	}

	public void playAudio (string audio){
		audioSource = GameObject.Find (audio).GetComponent<AudioSource>();
		audioSource.Play ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
