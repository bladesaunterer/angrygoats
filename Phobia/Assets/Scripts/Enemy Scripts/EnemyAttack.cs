using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    

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
