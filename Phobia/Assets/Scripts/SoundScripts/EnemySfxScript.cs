using UnityEngine;
using System.Collections;

/// <summary>
/// Script for playing SFX related to enemies.
/// </summary>
public class EnemySfxScript : MonoBehaviour
{
    private AudioSource sound;

    static private EnemySfxScript instance;

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

    // Methods for calling in other classes at the appropriate time.
    static public void playSound(AudioClip soundEffect)
    {
        instance.sound.PlayOneShot(soundEffect);
    }
}