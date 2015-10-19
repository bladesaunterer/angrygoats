using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Allows certain enemies to push the player and deal damage to them on contact.<para/>
/// Author:
/// </summary>
public class EnemyPush : MonoBehaviour
{
    public int knockback;

    void OnTriggerEnter(Collider other)
    {
        // If comes into contact with the player add a force to the player
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * knockback);
            }
        }
    }
}
