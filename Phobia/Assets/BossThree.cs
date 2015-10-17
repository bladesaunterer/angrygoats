using UnityEngine;
using System.Collections;

public class BossThree : MonoBehaviour {

	public RoomControl roomCont;
	public GameObject ghost;
	public int quantPerWave;
	int lastHP;
	int flag = 0;
	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (roomCont != null && flag == 0) {
			flag = 1;
			for (int i = 0; i < quantPerWave; i++) {
				roomCont.AddEnemy(ghost);
				Debug.Log("Hit!");
			}
		}
		if(flag == 1 &&  (this.gameObject.GetComponent<EnemyHealth> ().currentHealth != this.gameObject.GetComponent<EnemyHealth> ().startingHealth)) {
			GameObject player = GameObject.FindWithTag("Player");
			Vector3 temp = player.transform.rotation * Vector3.forward * -1;
			player.GetComponent<Rigidbody>().AddForce(temp*3000);
			this.gameObject.GetComponent<EnemySpinShot>().shoot = 1;
			lastHP = this.gameObject.GetComponent<EnemyHealth> ().currentHealth;
			flag = 2;
		}

		if (flag == 2 && (lastHP != this.gameObject.GetComponent<EnemyHealth> ().currentHealth)) {
			flag = 3;
			this.gameObject.GetComponent<EnemySpinShot>().shoot = 0;
			for (int i = 0; i < quantPerWave; i++) {
				GameObject enemy = roomCont.AddEnemy(ghost);
				Vector3 temppos;
				temppos = this.gameObject.transform.position;
				this.gameObject.transform.position = enemy.transform.position;
				enemy.transform.position = temppos;
			}
		}
		
	}
}
