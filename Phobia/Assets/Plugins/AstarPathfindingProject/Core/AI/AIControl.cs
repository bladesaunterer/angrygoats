using UnityEngine;
using System.Collections;

public class AIControl : MonoBehaviour {
	private bool playerInRange;
	private GameObject play;
	private float timeBetweenAttacks = 0.25f;
	float timer;
	
	void Awake ()
	{
		playerInRange = false;
	}
	void OnTriggerEnter (Collider other)
	{
		// When colliding with player, damage the player.
		if (play == null) {
			if (other.gameObject.CompareTag("Player")){
				play = other.gameObject;
			}
		}
		if (other.gameObject.CompareTag ("Player")) {
			playerInRange = true;
		}
	}
	
	void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			playerInRange = false;
		}
	}
	
	void Update(){
		timer += Time.deltaTime;
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			Attack (play);
		}
	}
	
	void Attack(GameObject other)
	{
		timer = 0f;
		if (other != null) {
			// PUSH!
		}
		
	}
}
