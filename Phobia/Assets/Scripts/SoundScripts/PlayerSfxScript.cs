using UnityEngine;
using System.Collections;

/// <summary>
/// Script for playing SFX sounds related to the player.
/// </summary>
public class PlayerSfxScript : MonoBehaviour {

    private AudioSource sound;

    public AudioClip meleeSound;
    // Sounds for each type of gem
    public AudioClip fireGemSound;
    public AudioClip iceGemSound;
    public AudioClip healGemSound;
    public AudioClip stealthGemSound;
    public AudioClip lightningGemSound;
    public AudioClip aoeGemSound;

    // Sound for character
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

    static public void playShotSound(Gem gem)
    {
        float vol = Random.Range(instance.volLowRange, instance.volHighRange);
        
        // Play the corresponding sound
        switch (gem)
        {
            case Gem.Red:
                instance.sound.PlayOneShot(instance.fireGemSound, vol);
                break;
            case Gem.Green:
                instance.sound.PlayOneShot(instance.healGemSound, vol);
                break;
            case Gem.Blue:
                instance.sound.PlayOneShot(instance.iceGemSound, vol);
                break;
            case Gem.Purple:
                instance.sound.PlayOneShot(instance.stealthGemSound, vol);
                break;
            case Gem.Yellow:
                instance.sound.PlayOneShot(instance.lightningGemSound, vol);
                break;
            default:
                instance.sound.PlayOneShot(instance.aoeGemSound, vol);
                break;
        }
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
