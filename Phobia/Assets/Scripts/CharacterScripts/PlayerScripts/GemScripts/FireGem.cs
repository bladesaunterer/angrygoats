using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: The red/fire gem. Creates a single bolt which applies a damage over time curse to whatever it hits. <para/>
/// Authors:
/// </summary>
public class FireGem : GenericGem
{
    // Damage amount
    public int overTimeDamage = 5;

    protected override void doEffect()
    {
        playerControl.SubtractCooldown(cost);
        GameObject shotSpawned = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
        shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
    }

    public override void onEnemyHit(GameObject other)
    {
        // Make sure the dot starts 1 second after the initial damage;
        base.onEnemyHit(other);
        endTime += 1;

        // Apply curse effect to enemy
        FireCurse curse = other.GetComponent<FireCurse>();
        if (curse == null)
        {
            curse = other.AddComponent<FireCurse>();
        }
        curse.updateStats(endTime, overTimeDamage);
    }

    // Uses the basic cast animation
    public override void castAnimation()
    {
        base.castAnimation();
    }
}
