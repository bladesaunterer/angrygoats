using UnityEngine;
using System.Collections;

/// <summary>
/// This is a singleton class in charge of controlling what music is played.
/// </summary>
public class MusicControlScript : MonoBehaviour
{
    public AudioClip menuMusic;
    public AudioClip levelMusic;

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
        // If in any of the menu scenes, play menu music if not playing already.
        if (Application.loadedLevelName == "MainMenuSceneWithSound" || 
            Application.loadedLevelName == "LevelSelectSceneWithSound" ||
            Application.loadedLevelName == "SettingsSceneWithSound")
        {
            if (instance.playingMusic.clip != instance.menuMusic)
            {
                PlayMusic(instance.menuMusic);
            }
        } else
        // Must be in a level, so play level music.
        {
            if (instance.playingMusic.clip != instance.levelMusic)
            {
                PlayMusic(instance.levelMusic);
            }
        }
    }

    // Plays the given audio clip.
    static private void PlayMusic(AudioClip music)
    {
        if (instance != null)
        {
            if (instance.playingMusic != null)
            {
                instance.playingMusic.Stop();
                instance.playingMusic.clip = music;
                instance.playingMusic.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
}