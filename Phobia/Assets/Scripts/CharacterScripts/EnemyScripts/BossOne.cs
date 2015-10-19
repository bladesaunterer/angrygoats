using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: A unique class for controlling the spider boss and it's abilities.<para/>
/// </summary>
public class BossOne : MonoBehaviour
{
	public GameObject enemyTypeOne;
	public GameObject enemyTypeTwo;
	public float spawnRate = 5f;

	float lastSpawnTime = 0;
	int nextSpawnType = 0;

	Vector3 left = new Vector3 (2, 0, 0);
	Vector3 right = new Vector3 (-2, 0, 0);
	Vector3 up = new Vector3 (0, 0, 2);
	Vector3 down = new Vector3 (0, 0, -2);

	EnemyHealth enemyHealth;

	private int numEnemiesSpawned = 0;

	// Use this for initialization
	void Start ()
	{
		enemyHealth = this.gameObject.GetComponent<EnemyHealth> ();
	}

	// Update is called once per frame
	void Update ()
	{
		// Checks to make sure the boss is still alive
		if (enemyHealth.currentHealth < enemyHealth.startingHealth) {
			float currentSpawnRate = spawnRate;

			// Adjust spawn rate based on the number of enemies spawned and spawn rate
			if (numEnemiesSpawned <= 3) {
				currentSpawnRate = spawnRate - 2;
			} else if (numEnemiesSpawned <= 7) {
				currentSpawnRate = spawnRate;
			} else {
				currentSpawnRate = spawnRate + 2;
			}

			// Spawn new spider minions
			if (Time.time > lastSpawnTime + currentSpawnRate) {
				numEnemiesSpawned++;
				lastSpawnTime = Time.time;

				// Spawn type0 spider minions
				if (nextSpawnType == 0) {
					GameObject make = (GameObject)GameObject.Instantiate (enemyTypeOne, this.gameObject.transform.position + left, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make.GetComponent<EnemyControl> ().home = transform;
					make = (GameObject)GameObject.Instantiate (enemyTypeOne, this.gameObject.transform.position + right, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make.GetComponent<EnemyControl> ().home = transform;

					nextSpawnType = 1;
				}
                // Spawn type1 spider minions
                else {
					GameObject make = (GameObject)GameObject.Instantiate (enemyTypeTwo, this.gameObject.transform.position + up, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make.GetComponent<EnemyControl> ().home = transform;
					make = (GameObject)GameObject.Instantiate (enemyTypeTwo, this.gameObject.transform.position + down, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make.GetComponent<EnemyControl> ().home = transform;

					nextSpawnType = 0;
				}
			}
		}
	}
}
