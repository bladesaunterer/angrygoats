using UnityEngine;
using System.Collections;

public class FireGem : GenericGem {

	public int overTimeDamage = 5;
	
	private EnemyHealth health;

	void awake(){
		tick = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost)
		{
            PlayerSfxScript.playShotSound();
			playerControl.SubtractCooldown(cost);
			Instantiate(shot,shotSpawn.position,shotSpawn.rotation);
		}
		if (health != null &&  tick && Time.time > nextTime){
			nextTime = Time.time + 1f;
			if (Time.time <= endTime){
				health.TakeDamage(overTimeDamage);
			} else {
				tick = false;
			}
		}
	}
	
	
	public override void onEnemyHit(GameObject other){
		health = other.GetComponent<EnemyHealth>();
		base.onEnemyHit(other);
	}
}
