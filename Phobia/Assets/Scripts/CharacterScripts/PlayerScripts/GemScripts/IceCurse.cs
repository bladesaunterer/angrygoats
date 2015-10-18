using UnityEngine;
using System.Collections;

public class IceCurse : MonoBehaviour {

	private float endTime;
	private float factor;
	private AIPath path;
	
	private float prevSpeed;

	// Use this for initialization
	void Start () {
		path = gameObject.GetComponent<AIPath>();
		prevSpeed = path.speed;
		path.speed = prevSpeed * factor;
	}

	// Update is called once per frame
	void Update () {
		if (Time.time > endTime){
			path.speed = prevSpeed;
			Destroy(this);
		}
	}

	public void addFactor(float factor){
		this.factor = factor;
	}

	public void updateEndTime(float time){
		endTime = time;
	}

}
