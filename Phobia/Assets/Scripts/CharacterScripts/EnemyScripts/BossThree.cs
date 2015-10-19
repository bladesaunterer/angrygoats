using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/**
 * Control Script for Darkness Level Boss
 */
public class BossThree : MonoBehaviour {

	public RoomControl roomCont;
	public GameObject ghost;
	float timer = 0;
	private float timeBetweenSwaps = 1f;
	public int quantPerWave;
	List<GameObject> ghosts = new List<GameObject>();
	int lastHP;
	int flag = 0;
	// Use this for initialization
	void Start () {
		ghosts.Add(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (roomCont != null && flag == 0) {
			flag = 1;
			lastHP = this.gameObject.GetComponent<EnemyHealth> ().startingHealth;
			for (int i = 0; i < quantPerWave; i++) {
				GameObject enemy = roomCont.AddEnemy(ghost);
				ghosts.Add(enemy);
				Debug.Log("Hit!");
			}
		}

		if (timer >= timeBetweenSwaps) {
			timer = 0f;
			foreach (GameObject ghost in ghosts) {
				if (ghost != null){
					if (roomCont == null){
						Debug.Log (";_;");
					}
				GameObject chosenCell = roomCont.freeCells [Random.Range (0, roomCont.freeCells.Count)];
				ghost.GetComponent<AIPath>().target = chosenCell.transform;
				Debug.Log("Swapping paths!");
				}
			}
		}

		if(flag == 1 &&  (this.gameObject.GetComponent<EnemyHealth> ().currentHealth != lastHP)) {
			GameObject player = GameObject.FindWithTag("Player");
			Vector3 temp = player.transform.rotation * Vector3.forward * -1;
			player.GetComponent<Rigidbody>().AddForce(temp*3000);
			foreach (GameObject ghost in ghosts) {
				if (ghost != null && ghost.tag != "Boss"){
					Destroy (ghost);
					Debug.Log("DIE!");
				}
			}
			this.gameObject.GetComponent<EnemySpinShot>().shoot = 1;
			lastHP = this.gameObject.GetComponent<EnemyHealth> ().currentHealth;
			this.gameObject.GetComponent<AIPath>().canMove = false;
			flag = 2;
		}

		if (flag == 2 && (lastHP != this.gameObject.GetComponent<EnemyHealth> ().currentHealth)) {
			flag = 1;
			lastHP = this.gameObject.GetComponent<EnemyHealth> ().currentHealth;
			this.gameObject.GetComponent<AIPath>().canMove = true;
			ghost.GetComponent<AIPath>().canMove = true;
			this.gameObject.GetComponent<EnemySpinShot>().shoot = 0;
			for (int i = 0; i < quantPerWave; i++) {
				GameObject enemy = roomCont.AddEnemy(ghost);
				ghosts.Add(enemy);
				Vector3 temppos;
				temppos = this.gameObject.transform.position;
				this.gameObject.transform.position = enemy.transform.position;
				enemy.transform.position = temppos;
			}
		}
		
	}
}
