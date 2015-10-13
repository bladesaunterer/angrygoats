using UnityEngine;
using System.Collections;

/*
 * This tests the slow of the ice gem
 * Attach this to the ice gem gameobject which is in Player/ShotSpawn/
 */
public class SlowGemTest : GemEffectTest {

	private float slow;

	private float prevSpeed;
	private float currentSpeed;
	private float newSpeed;

	void Awake(){
		slow = gameObject.GetComponent<IceGem>().slowSpeed;
	}

	// Update is called once per frame
	void Update () {
		if(other != null && Time.time > nextTime && Time.time <= maxTime){
			currentSpeed = other.GetComponent<AIPath>().speed;
			if (newSpeed != currentSpeed){
				IntegrationTest.Fail();
			} else {
				check = true;
			}
		}
		if(Time.time > (maxTime+0.1f) && check){
			if (prevSpeed == other.GetComponent<AIPath>().speed){
				IntegrationTest.Pass();
			} else {
				IntegrationTest.Fail();
			}
		}
	}

	public override void toggleOnHit(GameObject other){
		prevSpeed = other.GetComponent<AIPath>().speed + slow;
		newSpeed = prevSpeed - slow;
		base.toggleOnHit(other);
	}
}
