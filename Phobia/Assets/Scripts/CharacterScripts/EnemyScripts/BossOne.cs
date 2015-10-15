using UnityEngine;
using System.Collections;

public class BossOne : MonoBehaviour {
	public GameObject enemyTypeOne;
	public GameObject enemyTypeTwo;
	public float spawnRate = 5f;
	
	float lastSpawnTime = 0;
	int nextSpawnType = 0;
	Vector3 left = new Vector3 (2, 0, 0);
	Vector3 right = new Vector3 (-2, 0, 0);
	Vector3 up = new Vector3 (0, 0, 2);
	Vector3 down = new Vector3 (0, 0, -2);
	EnemyHealth eh;
	// Use this for initialization
	void Start () {
		eh = this.gameObject.GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		if (eh.currentHealth < eh.startingHealth) {
			if (Time.time > lastSpawnTime + spawnRate) {
				lastSpawnTime = Time.time;
				if (nextSpawnType == 0) {
					GameObject make = (GameObject)GameObject.Instantiate (enemyTypeOne, this.gameObject.transform.position + left, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make = (GameObject)GameObject.Instantiate (enemyTypeOne, this.gameObject.transform.position + right, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;

					nextSpawnType = 1;
				} else {
					GameObject make = (GameObject)GameObject.Instantiate (enemyTypeTwo, this.gameObject.transform.position + up, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make = (GameObject)GameObject.Instantiate (enemyTypeTwo, this.gameObject.transform.position + down, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					nextSpawnType = 0;
				}
			}
		}
	}
}
