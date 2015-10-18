using UnityEngine;
using System.Collections;

public class EnemySpinShot : MonoBehaviour {
	public GameObject shot; 
	public Transform shotSpawn;
	public Transform shotSpawn1;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	Quaternion temp;
	float timer;
	private float counter = 0;
	public int shoot = 1;
	private float timeBetweenAttacks = 0.3f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		counter++;
		if (timer >= timeBetweenAttacks && shoot == 1) {
			timer = 0f;
			temp = shotSpawn.rotation;
			temp *= Quaternion.Euler(0, counter, 0);
			shot.GetComponent<EnemyBolt> ().enemy = gameObject;
			Instantiate (shot, shotSpawn.position, temp);

			temp = shotSpawn1.rotation;
			temp *= Quaternion.Euler(0, counter, 0);
			shot.GetComponent<EnemyBolt> ().enemy = gameObject;
			Instantiate (shot, shotSpawn.position, temp);

			temp = shotSpawn2.rotation;
			temp *= Quaternion.Euler(0, counter, 0);
			shot.GetComponent<EnemyBolt> ().enemy = gameObject;
			Instantiate (shot, shotSpawn.position, temp);

			temp = shotSpawn3.rotation;
			temp *= Quaternion.Euler(0, counter, 0);
			shot.GetComponent<EnemyBolt> ().enemy = gameObject;
			Instantiate (shot, shotSpawn.position, temp);

			//It's not duplicate code see the numbers are different.
		}
	}
}
