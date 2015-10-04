using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	Transform player;               // Reference to the player's position.
	//PlayerHealth playerHealth;      // Reference to the player's health. (script attached to player in tutorial)
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	
	void Awake()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		//playerHealth = player.GetComponent<PlayerHealth>();
		//enemyHealth = GetComponent<EnemyHealth>();
		nav = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		// If the enemy and the player have health left...
		//if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
		//{
		// ... set the destination of the nav mesh agent to the player.
		if (player != null) {
			nav.SetDestination (player.position);
		}
		//}
		// Otherwise...
		//else
		//{
		// ... disable the nav mesh agent.
		//    nav.enabled = false;
		//}
	}

	void OnCollisionEnter(Collision other)
	{
		//if (other.gameObject.CompareTag ("Player")) {
			HealthControl.dealDamageToPlayer(other.gameObject, 8);
		//}
	}






    void onDestroy()
    {
        Debug.Log("Enemy Destroyed!");
        if (this.tag == "Enemy")
        {
            
            TEMPScoreScript.Instance.IncrementScore(10);
        }
    }
}
