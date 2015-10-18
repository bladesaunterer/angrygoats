using UnityEngine;
using System.Collections;

/// <summary>
/// Script for playing SFX related to enemies.
/// </summary>
public class EnemySfxScript : MonoBehaviour
{

    private AudioSource sound;
    public AudioClip meleeSound;
    public AudioClip shotSound;
    public AudioClip deathSound;
    public AudioClip hitSound;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    static private EnemySfxScript instance;

    void Start()
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
    static public void playMeleeSound()
    {
        float vol = Random.Range(instance.volLowRange, instance.volHighRange);
        instance.sound.PlayOneShot(instance.meleeSound, vol);
    }

    static public void playShotSound()
    {
        float vol = Random.Range(instance.volLowRange, instance.volHighRange);
        instance.sound.PlayOneShot(instance.shotSound, vol);
    }

    static public void playDeathSound()
    {
        instance.sound.PlayOneShot(instance.deathSound);
    }

    static public void playHitSound()
    {
        instance.sound.PlayOneShot(instance.hitSound);
    }
}