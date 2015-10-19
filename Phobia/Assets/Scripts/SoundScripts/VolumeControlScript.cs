using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Script used to adjust the background music and sfx volumes.
/// </summary>
public class VolumeControlScript : MonoBehaviour {

	public Slider musicSlider;
	public Slider sfxSlider;

	// Use this for initialization
	void Start () {
        // Add listeners for slider changes
		musicSlider.onValueChanged.AddListener (delegate {ChangeMusicVolume ();});
		sfxSlider.onValueChanged.AddListener (delegate {ChangeSfxVolume ();});

        // If the player has changed the music volume before
        float previousMusicVolume = PlayerPrefs.GetFloat("musicVolume", -1.0f);
        if (previousMusicVolume != -1.0f)
        {
            // Set the music slider to that volume
            musicSlider.value = previousMusicVolume;
        }

        // If the player has changed the sfx volume before
        float previousSfxVolume = PlayerPrefs.GetFloat("sfxVolume", -1.0f);
        if (previousSfxVolume != -1.0f)
        {
            // Set the sfx slider to that volume
            sfxSlider.value = previousSfxVolume;
        }

    }

    // Uses MusicControlScript (BGM music script) to change the volume of the 
    // background music.
	private void ChangeMusicVolume()
	{
        MusicControlScript.ChangeMusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
	}

    // Changes the sfx volume and sets them in PlayerPrefs.
    // Uses the master volume (AudioListener) which controls all volume in game.
    // Music volume is ignored because a boolean (IgnoreAudioListener) is set to true.
	private void ChangeSfxVolume()
	{
        AudioListener.volume = sfxSlider.value;
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }
}
