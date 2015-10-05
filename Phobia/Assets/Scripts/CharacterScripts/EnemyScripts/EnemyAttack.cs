using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles enemy attack logic.
 * 
 **/
public class EnemyAttack : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        // When colliding with player, damage the player.
        HealthControl.dealDamageToPlayer(other.gameObject, 8);
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
