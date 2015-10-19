using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: The ice/blue gem. Useful for slowing enemies for a prolonged period of time so that you can make your get away.<para/>
/// Authors:
/// </summary>
public class IceGem : GenericGem
{

    public float factor = 0.5f;

    protected override void doEffect()
    {
        playerControl.SubtractCooldown(cost);
        GameObject shotSpawned = Instantiate(shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
        shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
    }

    public override void onEnemyHit(GameObject other)
    {
        base.onEnemyHit(other);

        // Applies the ice curse upon the enemy it hits.
        IceCurse curse = other.GetComponent<IceCurse>();
        if (curse == null)
        {
            curse = other.AddComponent<IceCurse>();
            curse.addFactor(factor);
        }
        curse.updateEndTime(endTime);
    }

    // Uses the basic cast animation
    public override void castAnimation()
    {
        base.castAnimation();
    }
}
