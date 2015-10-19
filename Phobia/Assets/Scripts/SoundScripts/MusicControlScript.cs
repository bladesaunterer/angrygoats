using UnityEngine;
using System;

/// <summary>
/// Script for controlling background music for different scenes.
/// Author: Karen Xie
/// </summary>
public class MusicControlScript : MonoBehaviour
{
    public AudioClip menuMusic;
    public string[] menuScenes;

    public AudioClip cutsceneMusic;
    public string[] cutsceneScenes;

    public AudioClip levelMusic;
    public string[] levelScenes;

    // Component that plays the music
    private AudioSource playingMusic;

    static private MusicControlScript instance;

    // Creates singleton on first run.
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
            this.playingMusic = GetComponent<AudioSource>();
            this.playingMusic.ignoreListenerVolume = true;
            DontDestroyOnLoad(this);
        }
        else
        {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        }
    }

    // Plays menu music on start.
    void Start ()
    {
        PlayMusic(instance.menuMusic);
    }

    // Checks which level is loaded and plays the approriate music.
    void Update ()
    {
        string loadedLevel = Application.loadedLevelName;

        // If in any of the menu scenes, play menu music if not playing already.
        if (Array.IndexOf(menuScenes, loadedLevel) != -1)
        {
            PlayMusic(menuMusic);

        } else if (Array.IndexOf(levelScenes, loadedLevel) != -1)
        {
            PlayMusic(levelMusic);
 
        } else if (Array.IndexOf(cutsceneScenes, loadedLevel) != -1)
        {
            PlayMusic(cutsceneMusic);
        }
    }

    // Plays the given audio clip if not playing already.
    static private void PlayMusic(AudioClip music)
    {
        if (instance != null)
        {
            if (instance.playingMusic != null)
            {
                // Set music to previously set music volume if any.
                float previousVolume = PlayerPrefs.GetFloat("musicVolume", -1.0f);
                if (previousVolume != -1.0f && instance.playingMusic.volume != previousVolume)
                {
                    instance.playingMusic.volume = previousVolume;
                }

                // Only switch the clip if it's not playing already.
                if (instance.playingMusic.clip != music)
                {
                    // Switch the currently playing clip to the given one.
                    instance.playingMusic.Stop();
                    instance.playingMusic.clip = music;
                    instance.playingMusic.Play();
                }
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }

	// Increases the music volume. Used by VolumeControlScript.
	static public void ChangeMusicVolume(float newValue) 
	{
		instance.playingMusic.volume = newValue;
	}
}