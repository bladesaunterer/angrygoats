using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Class which handles enemy attack logic.
/// Authors: 
/// 
/// 
/// </summary>
public class SpiderAttack : MonoBehaviour
{
    // Balance variables
    public int damage = 8;
    public float timeBetweenAttacks = 0.25f;

    private bool playerInRange;
    private GameObject player;
    private float timer;

    // When initally spawned the player is not in range
    void Awake()
    {
        playerInRange = false;
    }

    void Update()
    {
        // Increment time to enable attacking cooldowns
        timer += Time.deltaTime;

        // Check to see if attack is off cooldown, player is in range, and spider is still alive
        if (timer >= timeBetweenAttacks && playerInRange && GetComponent<EnemyHealth>().currentHealth > 0)
        {
            //Play attack animation and attack player  
            GetComponent<SpiderAnimation>().attackAnim(); // TODO Change to ANIMATOR
            Attack(player);
        }
    }

    // When colliding with an object, check to see if it is the player
    void OnTriggerEnter(Collider other)
    {
        // If first time coliding with a player, store a reference to the player object
        if (player == null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player = other.gameObject;
            }
        }
        // Put the player "inRange"
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    // After colliding wih the player, set it as no longer "inRange"
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Attack(GameObject other)
    {
        // Set timer to zero to indicate cooldown
        timer = 0f;

        if (other != null)
        {
            // Deal damage to player
            Debug.Log("Enemy deals " + damage.ToString() + "damage!");
            HealthControl.dealDamageToPlayer(other, damage);
        }
    }
}
