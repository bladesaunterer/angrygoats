using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: A more advance shooting mechanism given to a boss to produce several bullets.<para/>
/// </summary>
public class EnemySpinShot : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public Transform shotSpawn1;
    public Transform shotSpawn2;
    public Transform shotSpawn3;

    Quaternion temp;
    float timer;
    private float counter = 0;
    public int shoot = 1;
    private float timeBetweenAttacks = 0.7f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        counter++;
        // Shoot the shots every x seconds
        if (timer >= timeBetweenAttacks && shoot == 1)
        {
            // Set timer to zero
            timer = 0f;

            // Spawn the four showts at the different locations
            temp = shotSpawn.rotation;
            Shoot(temp);
            temp = shotSpawn1.rotation;
            Shoot(temp);
            temp = shotSpawn2.rotation;
            Shoot(temp);
            temp = shotSpawn3.rotation;
            Shoot(temp);
        }
    }

    void Shoot(Quaternion temp)
    {
        temp *= Quaternion.Euler(0, counter, 0);
        shot.GetComponent<EnemyBolt>().enemy = gameObject;
        Instantiate(shot, shotSpawn.position, temp);
    }
}
