using UnityEngine;
using System.Collections;

public class LightningGem : GenericGem {

	public int damage = 10;
	public int duration = 5;
	public int cost = 25;

	private bool tick;
	private float endTime;
	private float nextTime;
	private AIPath ai;


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost)
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
		EnemyHealth health = other.GetComponent<EnemyHealth>();
		ai = other.GetComponent<AIPath>();
		if (health != null){
			health.TakeDamage(damage);
			tick = true;
			endTime = Time.time + duration;
		}
	}
}
