using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles enemy attack logic.
 * 
 **/
public class EnemyAttack : MonoBehaviour
{
	bool playerInRange;
	GameObject play;

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
		if (playerInRange)
		{
			Attack (play);
		}
	}

	void Attack(GameObject other)
	{
			if (other != null) {
			HealthControl.dealDamageToPlayer (other, 1);
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
