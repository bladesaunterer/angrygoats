using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: The basic single shot abilty an enemy can have attached to it.<para/>
/// Author:
/// </summary>
public class EnemySingleShot : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public bool shouldShoot = true;
    float timer;
    private float timeBetweenAttacks = 1f;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<EnemyAnimatorFinding>().getEnemyAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Shoots after a certain time between attacks
        if (timer >= timeBetweenAttacks && shouldShoot)
        {
            timer = 0f;
            if (anim != null)
            {
                EnemyAnimatorController.ExecuteAnimation(anim, "Cast");
            }
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }
}