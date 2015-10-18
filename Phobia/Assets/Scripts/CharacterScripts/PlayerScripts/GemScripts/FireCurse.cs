using UnityEngine;
using System.Collections;

public class FireCurse : MonoBehaviour {

	private float endTime;
	private EnemyHealth health;
	private int overTime;

	private float nextTime;

	// Use this for initialization
	void Start () {
		health = gameObject.GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextTime){
			nextTime = Time.time + 1;
			if (Time.time > endTime){
				Destroy(this);
			} else {
				health.TakeDamage(overTime);
			}
		}
	}

	public void updateStats(float time, int damage){
		endTime = time;
		overTime = damage;
		nextTime = Time.time + 1;
	}
}
