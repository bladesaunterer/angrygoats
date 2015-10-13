using UnityEngine;
using System.Collections;

/*
 * Generic class to test the effect of gems
 * This script should not be attached to any game object.
 */
public class GemEffectTest : MonoBehaviour {
	
	protected GameObject other;
	protected float nextTime;
	protected float maxTime;
	protected int duration;
	//This variable is used so math can be done easily
	protected int count;
	//This variable will check if the gem behaves the way it is suppose to
	protected bool check;

	void Start(){
		duration = gameObject.GetComponent<GenericGem>().duration;
		count = duration - 1;
		check = false;
	}

	//Method is called by the bolt once the bolt hits an enemy - called by InitialDamageTest
	public virtual void toggleOnHit(GameObject other){
		this.other = other;
		maxTime = Time.time + duration;
	}
}
