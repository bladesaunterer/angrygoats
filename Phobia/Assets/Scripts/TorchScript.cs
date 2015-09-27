using UnityEngine;
using System.Collections;

public class TorchScript : MonoBehaviour {
	
	private float timeTillChange = 0f;
	private float changeRate = 0f;
	private const float MAX_CHANGE_RATE = 1f;
	private const float MAX_TIME_STEP = 1f;
	private const float MIN_INTENSITY = 0.8f;
	private const float MAX_INTENSITY = 1.3f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time > timeTillChange) {
			timeTillChange = Time.time + (float) (Random.value*MAX_TIME_STEP);
			changeRate = ((2*Random.value-1)*MAX_CHANGE_RATE) * Time.deltaTime;
		}
		
		Light light = GetComponent<Light>();
		float newIntensity = (float) (light.intensity + changeRate);
		
		if (newIntensity > MAX_INTENSITY) {
			newIntensity = MAX_INTENSITY;
		} else if (newIntensity < MIN_INTENSITY) {
			newIntensity = MIN_INTENSITY;
		}
		
		light.intensity = newIntensity;
	}
}
