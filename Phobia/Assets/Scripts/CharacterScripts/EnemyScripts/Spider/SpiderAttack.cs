using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles enemy attack logic.
 * 
 **/
public class SpiderAttack : MonoBehaviour
{

    public int damage = 8;


    private bool playerInRange;
    private GameObject player;
    private float timeBetweenAttacks = 0.25f;
    float timer;

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
            //Play attack animation
            GetComponent<SpiderAnimation>().attackAnim();

            Attack(player);
        }
    }

    void Attack(GameObject other)
    {
        timer = 0f;
        if (other != null)
        {
            //Deal damage to player
            HealthControl.dealDamageToPlayer(other, damage);
        }

    }

    //void onDestroy()
    //{
    //    Debug.Log("Enemy Destroyed!");
    //    if (this.tag == "Enemy")
    //    {
    //        // When enemy destroyed, increment score.
    //        TEMPScoreScript.Instance.IncrementScore(10);
    //    }
    //}

    
}
