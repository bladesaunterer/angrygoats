using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles enemy attack logic.
 * 
 **/
public class EnemyAttack : MonoBehaviour
{
	private bool playerInRange;
	private GameObject play;
	private float timeBetweenAttacks = 0.5f;
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
			HealthControl.dealDamageToPlayer (other, 8);
		}

	}

	void onDestroy()
    {
        Debug.Log("Enemy Destroyed!");
        if (this.tag == "Enemy")
        {
			// When enemy destroyed, increment score.
            TEMPScoreScript.Instance.IncrementScore(10);
        }
    }
}
