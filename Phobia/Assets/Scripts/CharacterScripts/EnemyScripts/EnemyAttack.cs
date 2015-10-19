using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Handles the enemy attack logic, triggering sounds and animations.<para/>
/// Authors:
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    public AudioClip attackSound;
    public int damage = 8;

    private bool playerInRange;
    private GameObject player;
    private float timeBetweenAttacks = 0.25f;
    private Animator anim;

    float timer;

    void Start()
    {
        // Get the animator component
        try
        {
            anim = GetComponent<EnemyAnimatorFinding>().getEnemyAnimator();
        }
        catch
        {
            Debug.Log("Could not find EnemyAnimatorFinding Script attached!");
        }

    }

    // Make sure the player is initially set out of range
    void Awake()
    {
        playerInRange = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check there is a reference to the player
        if (player == null)
        {
            // Set a reference to the player
            if (other.gameObject.CompareTag("Player"))
            {
                player = other.gameObject;
            }
        }
        // Set player to be in range
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Set player to be out of range
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        // If the enemy can attack, the player is in range and the enemy still has health, execute attack.
        if (timer >= timeBetweenAttacks && playerInRange && GetComponent<EnemyHealth>().currentHealth > 0)
        {
            // If there is a sound associated, play it
            if (attackSound != null)
            {
                SfxScript.playSound(attackSound);
            }

            // If there is an animation associated, play it
            if (anim != null)
            {
                EnemyAnimatorController.ExecuteAnimation(anim, "Attack");
            }
            else
            {
                // Exclusive animation for the spider model
                attackAnimation();
            }
            Attack(player);
        }
    }

    void Attack(GameObject other)
    {
        timer = 0f;
        if (other != null)
        {
            // Deal damage to player
            HealthControl.dealDamageToPlayer(other, damage);
        }

    }

    // Used for animation executions
    protected virtual void attackAnimation()
    {

    }
}
