using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Purpose: A class to control the generic behaviour of all gems and their interations<para/>
/// Authors:
/// </summary>
public class GenericGem : MonoBehaviour
{
    protected Animator anim;

    // Basic fields
    public int damage = 10;
    public int duration = 5;
    public int cost = 25;
    public bool isCurrent = true;
    public Material boltMaterial;
    public Material staffMaterial;
    public Material staffParticles;

    protected GameObject player;
    protected PlayerControl playerControl;

    protected GameObject shot;                  // The special attack object.
    protected Transform shotSpawn;              // Location where the special attack will spawn.

    protected float endTime;

    void Start()
    {
        // Find and reference the player and shot
        player = GameObject.FindGameObjectWithTag("Player");
        playerControl = player.GetComponent<PlayerControl>();
        shot = playerControl.shot;
        shotSpawn = playerControl.shotSpawn;

        // Enables animations
        anim = player.GetComponent<Animator>();

    }

    void Update()
    {
        // Cast ability when K is pressed then there is enough mana/energy
        if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost && isCurrent)
        {
            // Update achievement log, play sound and execute associated gem ability
            AchievementManager.Instance.usedGem();
            PlayerSfxScript.playShotSound();
            doEffect();
            castAnimation();
        }
    }

    /**
	 * Method gets called when k button is pressed
	 */
    protected virtual void doEffect()
    {

    }

    /**
	 * Method gets called by the bolt when it hits an enemy
	 */
    public virtual void onEnemyHit(GameObject other)
    {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.TakeDamage(damage);
            endTime = Time.time + duration;
        }
    }

    /**
    * Method gets called which animates the player based on the gem used. (Base method is "projectile")
    */
    public virtual void castAnimation()
    {
        anim.SetTrigger("ShootSpell");
    }
}
