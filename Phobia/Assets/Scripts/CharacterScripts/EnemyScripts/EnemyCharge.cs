using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Applies a charge ability to certain enemies to give them super speed abilities.<para/>
/// Authors:
/// </summary>
public class EnemyCharge : MonoBehaviour
{
    public int scale = 100;

    // Update is called once per frame
    void Update()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if (Physics.Raycast(transform.position, fwd))
        {
            // Cause the enemy to charge forward
            this.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * scale);
        }
    }
}
