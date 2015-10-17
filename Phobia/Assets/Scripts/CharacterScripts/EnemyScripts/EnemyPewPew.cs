using UnityEngine;
using System.Collections;

public class EnemyPewPew : MonoBehaviour {
	public GameObject shot;
	public int shoot = 1;
	public Transform shotSpawn;
	public Transform shotSpawn1;
	public Transform shotSpawn2;
	public Transform shotSpawn3;
	float timer;
	private float timeBetweenAttacks = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= timeBetweenAttacks && shoot == 1) {
			timer = 0f;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			Instantiate (shot, shotSpawn1.position, shotSpawn1.rotation);
			Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);
			Instantiate (shot, shotSpawn3.position, shotSpawn3.rotation);
		}
	}
}
