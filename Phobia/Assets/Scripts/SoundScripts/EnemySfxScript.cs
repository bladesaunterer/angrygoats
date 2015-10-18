using UnityEngine;
using System.Collections;

public class EnemySfxScript : MonoBehaviour {

    private AudioSource attackSound;

    public AudioClip walkingSound;
    public AudioClip shortRangeAttackSound;
    public AudioClip longRangeAttackSound;

    private float volLowRange = .5f;
    private float volHighRange = 1.0f;

    // Use this for initialization
    void Start () {
        this.attackSound = GetComponentInChildren<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    // Methods for calling in other classes at the appropriate time.
    public void playShortRangeAttackSound()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        attackSound.PlayOneShot(shortRangeAttackSound, vol);
    }

    public void playLongRangeAttackSound()
    {
        float vol = Random.Range(volLowRange, volHighRange);
        attackSound.PlayOneShot(longRangeAttackSound, vol);
    }
}
