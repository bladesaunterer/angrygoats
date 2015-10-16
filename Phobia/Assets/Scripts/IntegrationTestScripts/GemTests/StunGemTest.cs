using UnityEngine;
using System.Collections;

/*
 * This tests the stun of the lightning gem
 * Attach this to the lightning gem gameobject which is in Player/ShotSpawn/
 */
public class StunGemTest : GemEffectTest {

	private AIPath ai;

	// Update is called once per frame
	void Update () {
		if(other != null && Time.time > nextTime && Time.time <= maxTime){
			if (ai.enabled){
				IntegrationTest.Fail();
			} else {
				check = true;
				nextTime++;
			}
		}
		if(Time.time > (maxTime+0.1f) && check){
			if (ai.enabled){
				IntegrationTest.Pass();
			} else {
				IntegrationTest.Fail();
			}
		}
	}

	public override void toggleOnHit(GameObject other){
		ai = other.GetComponent<AIPath>();
		base.toggleOnHit(other);
		nextTime = Time.time + 0.1f;;
	}
}
