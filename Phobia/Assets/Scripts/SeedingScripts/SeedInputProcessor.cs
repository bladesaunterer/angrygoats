using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using Pathfinding;
using System.Linq;
using System;

/**
 * Class used to handle UI inputs for seeded level generation.
 * Makes required checks to provided input, and when valid displays begin UI component.
 */
public class SeedInputProcessor : MonoBehaviour {
	
	public Button beginButton;
	
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
		// Repeated check to ensure nothing has changed since last process, as we are now generating.
		if (int.TryParse (actSeedInput.text, out actSeedInputInt) && int.TryParse (roomsToSpawn.text, out roomsToSpawnInt) && 
			int.TryParse (totalEnemies.text, out totalEnemiesInt) && int.TryParse (maxEnemiesPerRoom.text, out maxEnemiesPerRoomInt) &&
			int.TryParse (minWebs.text, out minWebsInt) && int.TryParse (maxWebs.text, out maxWebsInt)) {
			if (roomsToSpawnInt > 0 && minWebsInt<=maxWebsInt && totalEnemiesInt/roomsToSpawnInt<=maxEnemiesPerRoomInt) {
				// Selects the appropriate level generator, sets the seed PlayerPref for later reference.
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
	
	// Used for initalisation of the begin UI component.
	void Start () {
		beginButton.onClick.AddListener(() => Enter());
		beginButton.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// Checks if current inputs are valid integers generally speaking.
		if (int.TryParse (actSeedInput.text, out actSeedInputInt) && int.TryParse (roomsToSpawn.text, out roomsToSpawnInt) && 
		    int.TryParse (totalEnemies.text, out totalEnemiesInt) && int.TryParse (maxEnemiesPerRoom.text, out maxEnemiesPerRoomInt) &&
		    int.TryParse (minWebs.text, out minWebsInt) && int.TryParse (maxWebs.text, out maxWebsInt)) {
			// Checks if current inputs are valid inputs in our context, to avoid issues with generation.
			if (roomsToSpawnInt > 0 && minWebsInt<=maxWebsInt && totalEnemiesInt/roomsToSpawnInt<=maxEnemiesPerRoomInt){
				//Sets begin button to active dependant on the above.
				beginButton.gameObject.SetActive(true);
			} else {
				beginButton.gameObject.SetActive(false);
			}
		} else {
				beginButton.gameObject.SetActive(false);
		}
	}
}