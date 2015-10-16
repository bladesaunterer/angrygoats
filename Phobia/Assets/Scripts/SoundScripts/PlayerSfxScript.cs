using UnityEngine;
using System.Collections;

public class PlayerSfxScript : MonoBehaviour {

    private AudioSource attackSound;
    public AudioClip meleeSound;
    public AudioClip shotSound;
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
        this.attackSound = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    // Methods for calling in other classes at the appropriate time.
    static public void playMeleeSound()
    {
        float vol = Random.Range(instance.volLowRange, instance.volHighRange);
        instance.attackSound.PlayOneShot(instance.meleeSound, vol);
    }

    static public void playShotSound()
    {
        float vol = Random.Range(instance.volLowRange, instance.volHighRange);
        instance.attackSound.PlayOneShot(instance.shotSound, vol);
    }
}
