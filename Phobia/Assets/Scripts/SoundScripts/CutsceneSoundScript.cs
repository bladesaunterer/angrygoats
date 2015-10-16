using UnityEngine;
using System.Collections;

/**
 *
 * This class is used to generate sound 
 * effects for cutscenes.
 *
 **/
public class CutsceneSoundScript : MonoBehaviour {

	public AudioClip textSound1;
	public AudioClip textSound2;
	public AudioClip textSound3;
	public AudioClip textSound4;

	private AudioSource audioSource;
	static private int num = 1;

	static private CutsceneSoundScript instance;
	
	void Awake () {
		if (instance == null) {
			instance = this;
		
		} else {
			// Self-destruct if another instance exists
			Destroy(this);
			return;
		}

		audioSource = GetComponent<AudioSource>();
	}

	/**
	 *
	 * Static method for playing text sound effects.
	 *
	 **/
	static public void PlayTextSound() {
		instance.audioSource.pitch = 1.0f;

		// Generate random number.
		num = Random.Range (1, 4);

		// Play random corresponding sound effect.
		if (num == 1) {
			instance.audioSource.PlayOneShot(instance.textSound1, 0.3f);

		} else if (num == 2) {
			instance.audioSource.PlayOneShot(instance.textSound2, 0.3f);

		} else if (num == 3) {
			instance.audioSource.PlayOneShot(instance.textSound3, 0.3f);

		} else if (num == 4) {
			instance.audioSource.PlayOneShot(instance.textSound4, 0.3f);
			
		}
	} 
}
