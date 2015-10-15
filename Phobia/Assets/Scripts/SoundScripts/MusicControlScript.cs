using UnityEngine;
using System;

/// <summary>
/// This is a singleton class in charge of controlling what music is played.
/// </summary>
public class MusicControlScript : MonoBehaviour
{
    public AudioClip menuMusic;
    public string[] menuScenes;

    public AudioClip cutsceneMusic;
    public string[] cutsceneScenes;

    public AudioClip level1Music;
    public string level1Scene;

    //public AudioClip level2Music;
    //public string level2Scene;

    //public AudioClip level3Music;
    //public string level3Scene;

    //public AudioClip winMusic;
    //public AudioClip loseMusic;

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
        if (Array.IndexOf(menuScenes, Application.loadedLevelName) != -1)
        {
            PlayMusic(menuMusic);

        } else if (Application.loadedLevelName == level1Scene)
        {
            PlayMusic(level1Music);
        }
        // Add more background sounds here if needed.
    }

    // Plays the given audio clip if not playing already.
    static private void PlayMusic(AudioClip music)
    {
        if (instance != null)
        {
            if (instance.playingMusic != null)
            {
                if (instance.playingMusic.clip != music)
                {
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
}