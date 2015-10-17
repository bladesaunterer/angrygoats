using UnityEngine;
using System.Collections;

public class EnemySingleShot : MonoBehaviour {

	public GameObject shot; 
	public Transform shotSpawn;
	public bool shouldShoot = true;
	float timer;
	private float timeBetweenAttacks = 1f;
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && shouldShoot) {
			timer = 0f;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}