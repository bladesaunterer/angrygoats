using UnityEngine;
using System.Collections;

public class BossControl : MonoBehaviour {
	
	Transform player;               // Reference to the player's position.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	
	void Awake()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag("Player").transform;
		nav = GetComponent<NavMeshAgent>();
	}
	
	void Update()
	{
		// Move to player's position.
		if (player != null) {
			nav.SetDestination (player.position);
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		// Deal damage to player on contact.
		HealthControl.dealDamageToPlayer(other.gameObject, 20);
	}
}
