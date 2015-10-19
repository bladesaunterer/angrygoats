using UnityEngine;
using System.Collections;

/// <summary>
/// Script used by other classes to play SFX.
/// Author: Karen Xie.
/// </summary>
public class SfxScript : MonoBehaviour
{
    private AudioSource sound;

    static private SfxScript instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            this.sound = GetComponentInChildren<AudioSource>();
        }
        else
        {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        }
    }

    // Check for previously set player sfx prefs.
    void Update()
    {
        float previousVolume = PlayerPrefs.GetFloat("sfxVolume", -1.0f);
        if (previousVolume != -1.0f && AudioListener.volume != previousVolume)
        {
            AudioListener.volume = previousVolume;
        }
    }

    // Methods for calling in other classes at the appropriate time.
    static public void playSound(AudioClip soundEffect)
    {
        instance.sound.PlayOneShot(soundEffect);
    }
}