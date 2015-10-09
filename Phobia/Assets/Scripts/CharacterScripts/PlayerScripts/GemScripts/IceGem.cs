using UnityEngine;
using System.Collections;

public class IceGem : GenericGem {

	public float slowSpeed = 30;

	private AIPath ai;
	private float prevSpeed;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost)
		{
			playerControl.SubtractCooldown(cost);
			Instantiate(shot,shotSpawn.position,shotSpawn.rotation);
		}
		if (ai != null &&  tick && Time.time > nextTime){
			nextTime = Time.time + 1f;
			if (Time.time > endTime){
				tick = false;
				ai.speed = prevSpeed;
			}
		}
	}

	public override void onEnemyHit(GameObject other){
		ai = other.GetComponent<AIPath>();
		if (ai != null && prevSpeed < ai.speed){
			prevSpeed = ai.speed;
			ai.speed -= slowSpeed;
		}
		base.onEnemyHit(other);
	}
}
