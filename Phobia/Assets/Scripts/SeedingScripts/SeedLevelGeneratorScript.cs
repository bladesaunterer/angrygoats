using UnityEngine;
using System.Collections;

public class SeedLevelGeneratorScript : MonoBehaviour {

	public GameObject SpiderLevelGenerator;
	public GameObject DarknessLevelGenerator;
	public GameObject HeightsLevelGenerator;
	public GameObject DarknessOverlay;

	// Use this for initialization
	void Start () {
		string seed = PlayerPrefs.GetString ("seed");
		print (seed);
		string[] parts = seed.ToString().Split('#');
		string level = parts [6];

		if (level == "SpiderLevelScene") {
			SpiderLevelGenerator.GetComponent<LevelGenerator>().seed = seed;
			// load spider generator
			SpiderLevelGenerator.SetActive(true);
		} else if (level == "HeightsLevelScene") {
			HeightsLevelGenerator.GetComponent<LevelGenerator>().seed = seed;
			//load heights generator
			HeightsLevelGenerator.SetActive(true);
		} else if (level == "DarknessLevelScene") {
			DarknessLevelGenerator.GetComponent<LevelGenerator>().seed = seed;
			//load darkness overlay
			DarknessOverlay.SetActive(true);
			//load darkness generator
			DarknessLevelGenerator.SetActive(true);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
