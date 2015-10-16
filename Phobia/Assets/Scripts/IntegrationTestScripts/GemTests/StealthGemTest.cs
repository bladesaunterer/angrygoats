using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Tests if stealth behaves correctly
 * Attach to player
 */
public class StealthGemTest : MonoBehaviour {

	private PlayerControl playerControl;
	private Transform playerTransform;

	private StealthGem gem;
	private GameObject enemy;
	private AIPath path;
	private EnemyControl enemyControl;

	private float startTime;
	private int duration;
	private float endTime;
	private bool check;

	// Use this for initialization
	void Start () {
		playerControl = GetComponent<PlayerControl>();
		playerTransform = gameObject.transform;
		GameObject temp = GameObject.FindGameObjectWithTag ("Black");
		gem = temp.GetComponent<StealthGem>();
		enemy = GameObject.FindGameObjectWithTag("Enemy");
		path = enemy.GetComponent<AIPath>();
		enemyControl = enemy.GetComponent<EnemyControl>();
		duration = gem.duration;
		startTime = Time.time + 2;
		check = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < startTime){
			if (playerTransform.position != path.target.position){
				IntegrationTest.Fail();
			}
		}
		if (Time.time > startTime && check){
			endTime = Time.time + duration;
			playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
			check = false;
		}
		if (!check && Time.time > endTime){
			playerControl.currentRoom.GetComponent<RoomControl>().EnemiesHuntPlayer();
			if (playerTransform.position != path.target.position){
				IntegrationTest.Fail();
			} else {
				IntegrationTest.Pass();
				check = true;
			}
		}
		if (!check){
			if (path.target.position != enemyControl.home.position){
				IntegrationTest.Fail();
			}
		}

	}
}
