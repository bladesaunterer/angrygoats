using UnityEngine;
using System.Collections;

public class FireGem : GenericGem {

	public int overTimeDamage = 5;

	protected override void doEffect(){
		playerControl.SubtractCooldown (cost);
		GameObject shotSpawned = Instantiate(shot,shotSpawn.position,shotSpawn.rotation) as GameObject;
		shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
	}
	
	public override void onEnemyHit(GameObject other){
		//Make sure the dot starts 1 second after the initial damage;
		base.onEnemyHit(other);
		endTime += 1;
		FireCurse curse = other.GetComponent<FireCurse>();
		if (curse == null){
			curse = other.AddComponent<FireCurse>();
		}
		curse.updateStats(endTime,overTimeDamage);
	}
}
