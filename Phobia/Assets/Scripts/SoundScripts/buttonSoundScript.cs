using UnityEngine;
using System.Collections;

public class buttonSoundScript : MonoBehaviour {

	AudioSource audioSource;

	public void playAudio (string audio){
		audioSource = GameObject.Find (audio).GetComponent<AudioSource>();
		if (audioSource != null){
		audioSource.Play ();
		}
	}
}
