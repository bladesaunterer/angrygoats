using UnityEngine;
using System.Collections;

/*
 * This tests the damage over time of the fire gem 
 * Attach this to the fire gem gameobject which is in Player/ShotSpawn/
 */
public class DamageOverTimeTest : GemEffectTest {
	private int damageOverTime;

	private int currentHealth;
	private int newHealth;

	void Awake(){
		damageOverTime = gameObject.GetComponent<FireGem>().overTimeDamage;
	}

	void Start(){
		GameObject shotSpawned = Instantiate(shot,shotSpawn.position,shotSpawn.rotation) as GameObject;
		shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);
		shotSpawned.AddComponent<InitialDamageTest>();
	}

	void Update(){
		if(other != null && Time.time > nextTime && Time.time <= maxTime){
			newHealth = other.GetComponent<EnemyHealth>().currentHealth;
			if (newHealth != currentHealth - damageOverTime*(duration - count)){
				IntegrationTest.Fail();
			} else {
				nextTime += 1;
				count--;
				check = true;
			}
		}
		if(Time.time > maxTime && check){
			IntegrationTest.Pass();
		}
	}

	public override void toggleOnHit(GameObject other){
		currentHealth = other.GetComponent<EnemyHealth>().currentHealth;
		base.toggleOnHit(other);
		nextTime = Time.time + 1.1f;
		maxTime += 1.1f;
	}
}
