using UnityEngine;
using System.Collections;

/**
 * Middleman class to handle seed processing, specifically selection of the appropriate generator.
 */

public class SeedLevelGeneratorScript : MonoBehaviour {

	public GameObject SpiderLevelGenerator;
	public GameObject DarknessLevelGenerator;
	public GameObject HeightsLevelGenerator;
	public GameObject DarknessOverlay;

	// Used for intialisation.
	void Awake () {
		// Obtain seed and parse
		string seed = PlayerPrefs.GetString ("seed");
		print (seed);
		string[] parts = seed.ToString().Split('#');
		string level = parts [6];
		// Select appropriate generator for seed.
		if (level == "SpiderLevelScene") {
			SpiderLevelGenerator.GetComponent<LevelGenerator>().seed = seed;
			// Load spider generator
			SpiderLevelGenerator.SetActive(true);
		} else if (level == "HeightsLevelScene") {
			HeightsLevelGenerator.GetComponent<LevelGenerator>().seed = seed;
			// Load heights generator
			HeightsLevelGenerator.SetActive(true);
		} else if (level == "DarknessLevelScene") {
			DarknessLevelGenerator.GetComponent<LevelGenerator>().seed = seed;
			// Load darkness overlay
			DarknessOverlay.SetActive(true);
			// Load darkness generator
			DarknessLevelGenerator.SetActive(true);
		}

	}
}
