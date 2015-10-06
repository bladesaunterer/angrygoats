using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles enemy attack logic.
 * 
 **/
public class EnemyAttack : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		Debug.Log("DO THE DMGS!");
		other.gameObject.GetComponent(PlayerHealth);
		PlayerHealth.TakeDamage(8);
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
