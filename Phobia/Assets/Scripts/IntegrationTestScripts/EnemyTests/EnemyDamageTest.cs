using UnityEngine;
using System.Collections;

/// <summary>
/// Test: The enemy does the correct amount of damage to the player.<para/>
/// Author:       <para/>
/// Notes: Attach this to the enemy game object
/// </summary>
public class EnemyDamageTest : MonoBehaviour
{
    private GameObject player;
    private float nextTime;

    // When colliding with a player
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            nextTime = Time.time + 0.25f;
        }
    }

    void Update()
    {
        if (player != null && Time.time > nextTime)
        {
			Debug.Log("Please");
			// Get players health and enemies damage
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            int damage = gameObject.GetComponent<EnemyAttack>().damage;

            // Compute asserted value
            float newHealth = health.startingHealth - damage;

            // Test asserted value
            if (health.currentHealth == newHealth)
            {
                IntegrationTest.Pass(gameObject);
            }
            else
            {
                IntegrationTest.Fail(gameObject);
            }
        }
    }
}
