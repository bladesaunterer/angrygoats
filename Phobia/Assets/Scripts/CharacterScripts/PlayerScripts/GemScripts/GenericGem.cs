using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenericGem : MonoBehaviour
{

	public int damage = 10;
	public int duration = 5;
	public int cost = 25;
	public bool isCurrent = true;
	public Material boltMaterial;
    public Material staffMaterial;
    public Material staffParticles;

	protected GameObject player;
	protected PlayerControl playerControl;

	protected GameObject shot;             		// The special attack object.
	protected Transform shotSpawn;         		// Location where the special attack will spawn.
	
	protected float endTime;


	// Use this for initialization


	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerControl = player.GetComponent<PlayerControl> ();
		shot = playerControl.shot;
		shotSpawn = playerControl.shotSpawn;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.K) && playerControl.cooldown >= cost && isCurrent) {
			AchievementManager.Instance.usedGem();
			PlayerSfxScript.playShotSound();
			doEffect();
		}
	}

	/**
	 * Method gets called when k button is pressed
	 */
	protected virtual void doEffect(){

	}

	/**
	 * Method gets called by the bolt when it hits an enemy
	 */
	public virtual void onEnemyHit (GameObject other) {
		EnemyHealth health = other.GetComponent<EnemyHealth> ();
		if (health != null) {
			health.TakeDamage (damage);
			endTime = Time.time + duration;
		}
	}
}
