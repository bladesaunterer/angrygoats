using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles enemy attack logic.
 * 
 **/
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
        try
        {
            anim = GetComponent<EnemyAnimatorFinding>().getEnemyAnimator();
        }
        catch
        {
            Debug.Log("Could not find EnemyAnimatorFinding Script attached!");
        }
        
    }

    void Awake()
    {
        playerInRange = false;
    }
    void OnTriggerEnter(Collider other)
    {
        // When colliding with player, damage the player.
        if (player == null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                player = other.gameObject;
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && GetComponent<EnemyHealth>().currentHealth > 0)
        {
            if (attackSound != null)
            {
                EnemySfxScript.playSound(attackSound);
            }

            if (anim != null)
            {
                EnemyAnimatorController.ExecuteAnimation(anim, "Attack");
            }
            else
            {
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
            HealthControl.dealDamageToPlayer(other, damage);
        }

    }

    protected virtual void attackAnimation()
    {

    }
}
