using UnityEngine;
using System.Collections;

public class LightningGem : GenericGem {
	private AIPath ai;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost && checkActive())
		{
			playerControl.SubtractCooldown(cost);
			Instantiate(shot,shotSpawn.position,shotSpawn.rotation);
		}
		if (ai != null &&  tick && Time.time > nextTime){
			nextTime = Time.time + 1f;
			if (Time.time <= endTime){
				ai.enabled = false;
			} else {
				tick = false;
				ai.enabled = true;
			}
		}
	}

	public override void onEnemyHit(GameObject other){
		ai = other.GetComponent<AIPath>();
		base.onEnemyHit(other);
	}
}
