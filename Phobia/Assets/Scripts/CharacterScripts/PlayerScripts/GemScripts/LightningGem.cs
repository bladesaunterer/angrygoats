using UnityEngine;
using System.Collections;

public class LightningGem : GenericGem {
	private AIPath ai;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost && isCurrent)
		{
			playerControl.SubtractCooldown(cost);
			GameObject shotSpawned = Instantiate(shot,shotSpawn.position,shotSpawn.rotation) as GameObject;
			shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
		}
		if (ai != null &&  tick && Time.time > nextTime){
			nextTime = Time.time + 1f;
			if (Time.time <= endTime){
				ai.canMove = false;
			} else {
				tick = false;
				ai.canMove = true;
			}
		}
	}

	public override void onEnemyHit(GameObject other){
		ai = other.GetComponent<AIPath>();
		base.onEnemyHit(other);
	}
}
