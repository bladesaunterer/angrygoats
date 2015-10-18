using UnityEngine;
using System.Collections;

public class EnemySfxScript : MonoBehaviour {

    private AudioSource sound;

    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip attackSound;

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start () {
        sound = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    // Methods for calling in other classes at the appropriate time.
    public void playHitSound()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        sound.PlayOneShot(hitSound, vol);
    }

    public void playDeathSound()
    {
        sound.PlayOneShot(deathSound);
    }

    public void playAttackSound()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        sound.PlayOneShot(attackSound, vol);
    }
}
