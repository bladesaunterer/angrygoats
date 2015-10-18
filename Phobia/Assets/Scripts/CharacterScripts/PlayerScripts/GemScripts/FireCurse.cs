using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Applies a curse to enemies which causes them to take damage over time. <para/>
/// Authors:
/// </summary>
public class FireCurse : MonoBehaviour
{

    private float endTime;
    private EnemyHealth health;
    private int overTime;

    private float nextTime;

    // Use this for initialization
    void Start()
    {
        health = gameObject.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {

        // Apply damage over time effect for a certain amount of time
        if (Time.time > nextTime)
        {
            nextTime = Time.time + 1;

            // When time expires lift the curse
            if (Time.time > endTime)
            {
                Destroy(this);
            }
            else
            {
                health.TakeDamage(overTime);
            }
        }
    }

    public void updateStats(float time, int damage)
    {
        endTime = time;
        overTime = damage;
        nextTime = Time.time + 1;
    }
}
