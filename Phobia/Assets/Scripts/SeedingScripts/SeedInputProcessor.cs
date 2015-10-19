using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;
using System;

public class SeedInputProcessor : MonoBehaviour {
	
	public Button beginButton;

//	public GameObject seedInput;
	public InputField actSeedInput;
	public InputField roomsToSpawn;
	public InputField totalEnemies;
	public InputField maxEnemiesPerRoom;
	public InputField minWebs;
	public InputField maxWebs;
	public Dropdown levelSelector;

	int actSeedInputInt;
	int roomsToSpawnInt;
	int totalEnemiesInt;
	int maxEnemiesPerRoomInt;
	int minWebsInt;
	int maxWebsInt;
	
	public void Enter (){
		if (int.TryParse (actSeedInput.text, out actSeedInputInt) && int.TryParse (roomsToSpawn.text, out roomsToSpawnInt) && 
			int.TryParse (totalEnemies.text, out totalEnemiesInt) && int.TryParse (maxEnemiesPerRoom.text, out maxEnemiesPerRoomInt) &&
			int.TryParse (minWebs.text, out minWebsInt) && int.TryParse (maxWebs.text, out maxWebsInt)) {
			if (minWebsInt <= maxWebsInt && totalEnemiesInt / roomsToSpawnInt < maxEnemiesPerRoomInt) {
				if (levelSelector.value == 0) {
					print ("Arachnophobia");
					PlayerPrefs.SetString ("seed", actSeedInput.text + "#" + roomsToSpawn.text + "#" + totalEnemies.text + "#" + maxEnemiesPerRoom.text + "#" + minWebs.text + "#" + maxWebs.text + "#" + "SpiderLevelScene");
				} else if (levelSelector.value == 1) {
					print ("Acrophobia");
					PlayerPrefs.SetString ("seed", actSeedInput.text + "#" + roomsToSpawn.text + "#" + totalEnemies.text + "#" + maxEnemiesPerRoom.text + "#" + minWebs.text + "#" + maxWebs.text + "#" + "HeightsLevelScene");
				} else if (levelSelector.value == 2) {
					print ("Nyctophobia");
					PlayerPrefs.SetString ("seed", actSeedInput.text + "#" + roomsToSpawn.text + "#" + totalEnemies.text + "#" + maxEnemiesPerRoom.text + "#" + minWebs.text + "#" + maxWebs.text + "#" + "DarknessLevelScene");
				} 
			}
		}
	}
	
	// Use this for initialization
	void Start () {
		beginButton.onClick.AddListener(() => Enter());
		beginButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetKeyDown(KeyCode.Return)){
//			Enter();
//		}

		if (int.TryParse (actSeedInput.text, out actSeedInputInt) && int.TryParse (roomsToSpawn.text, out roomsToSpawnInt) && 
		    int.TryParse (totalEnemies.text, out totalEnemiesInt) && int.TryParse (maxEnemiesPerRoom.text, out maxEnemiesPerRoomInt) &&
		    int.TryParse (minWebs.text, out minWebsInt) && int.TryParse (maxWebs.text, out maxWebsInt)) {
			if (minWebsInt<=maxWebsInt && totalEnemiesInt/roomsToSpawnInt<maxEnemiesPerRoomInt){
				//set begin button to active
				beginButton.gameObject.SetActive(true);
			} else {
				beginButton.gameObject.SetActive(false);
			}
		} else {
				beginButton.gameObject.SetActive(false);
		}
	}
}