using UnityEngine;
using System.Collections;

public class BossThree : MonoBehaviour {

	public RoomControl roomCont;
	public GameObject ghost;
	public int quantPerWave;
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

	}
}
