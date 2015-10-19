using UnityEngine;
using System.Collections;

public class buttonSoundScript : MonoBehaviour {

	AudioSource audioSource;

	public void playAudio (string audio){
		GameObject audioSourceObject = GameObject.Find (audio);
		if (audioSourceObject != null) {
			audioSourceObject.GetComponent<AudioSource> ();
			if (audioSource != null) {
				audioSource.Play ();
			}
		}
	}
}
