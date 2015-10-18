using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightningGem : GenericGem {

	protected override void doEffect(){
		playerControl.SubtractCooldown (cost);
		GameObject shotSpawned = Instantiate(shot,shotSpawn.position,shotSpawn.rotation) as GameObject;
		shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
	}

	public override void onEnemyHit(GameObject other){
		base.onEnemyHit(other);
		LightningCurse curse = other.GetComponent<LightningCurse>();
		if (curse == null){
			curse = other.AddComponent<LightningCurse>();
		}
		curse.updateEndTime(endTime);
	}
	
}
