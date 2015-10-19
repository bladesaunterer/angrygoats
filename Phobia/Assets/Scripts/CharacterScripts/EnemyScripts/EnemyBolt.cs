using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Creates a bolt attack which can be instantiated by enemies with casting abilities.<para/>
/// Authors:
/// </summary>
public class EnemyBolt : MonoBehaviour
{
    public float speed;
    public GameObject enemy;
    private Rigidbody rb;
    public int boltDamage;

    void Start()
    {

        // Get rigidbody and set bolt's velocity.
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bolt come into contact with a door, wall, boss, other enemy or player
        if (other.gameObject.CompareTag("Door")
            || other.gameObject.CompareTag("Wall")
        || (other.gameObject.CompareTag("Enemy") && (enemy != other.gameObject))
        || other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("Player"))
        {

            // Destroy bolt on contact.
            Destroy(gameObject);

            // If bolt hits a player, deal damage to the player.
            HealthControl.dealDamageToPlayer(other.gameObject, boltDamage);
        }
    }
}
