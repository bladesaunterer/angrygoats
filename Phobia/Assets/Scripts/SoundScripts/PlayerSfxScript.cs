using UnityEngine;
using System.Collections;

/// <summary>
/// Script for playing SFX sounds related to the player.
/// </summary>
public class PlayerSfxScript : MonoBehaviour {

    private AudioSource sound;
    public AudioClip meleeSound;
    public AudioClip shotSound;
    public AudioClip deathSound;
    public AudioClip hitSound;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    static private PlayerSfxScript instance;

    // Creates singleton on first run.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // Self-destruct if another instance exists
            Destroy(this);
            return;
        }
    }
    // Use this for initialization
    void Start () {
        this.sound = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

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
