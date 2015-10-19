using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Purpose: The yellow/lightning gem. Useful for stunning the enemy in their tracks.<para/>
/// This completely stops the enemy but is applied for a shorter time than the freeze gem
/// </summary>
public class LightningGem : GenericGem {

	protected override void doEffect(){
		playerControl.SubtractCooldown (cost);
		GameObject shotSpawned = Instantiate(shot,shotSpawn.position,shotSpawn.rotation) as GameObject;
		shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
	}

	public override void onEnemyHit(GameObject other){
		base.onEnemyHit(other);

        // Apply lightning curse to tht enemy the spell hits
		LightningCurse curse = other.GetComponent<LightningCurse>();
		if (curse == null){
			curse = other.AddComponent<LightningCurse>();
		}
		curse.updateEndTime(endTime);
	}

    // Uses the basic cast animation
    public override void castAnimation()
    {
        base.castAnimation();
    }

}
