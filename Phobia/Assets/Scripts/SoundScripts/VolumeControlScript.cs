using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VolumeControlScript : MonoBehaviour {

	public Slider musicSlider;
	public Slider sfxSlider;

	// Use this for initialization
	void Start () {
		musicSlider.onValueChanged.AddListener (delegate {ChangeMusicVolume ();});
		sfxSlider.onValueChanged.AddListener (delegate {ChangeSfxVolume ();});
	}

	private void ChangeMusicVolume()
	{
		MusicControlScript.ChangeMusicVolume (musicSlider.value);
	}

	private void ChangeSfxVolume()
	{
		PlayerSfxScript.ChangeSfxVolume (sfxSlider.value);
	}
}
