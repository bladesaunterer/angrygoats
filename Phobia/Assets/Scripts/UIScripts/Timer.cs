using UnityEngine;
using UnityEngine.UI;

/**
 * 
 * Class which handles timer logic on UI.
 * 
 **/
public class Timer : MonoBehaviour {

    Text timerUI;
    float timer = 0;
    public static bool isStarted;

    void Start()
    {
		// Get UI component and set 'isStarted' boolean to true.
        timerUI = GetComponent<UnityEngine.UI.Text>();
        isStarted = true;
    }

    void Update()
    {
        if (isStarted)
        {
			// Increment time and update the corresponding UI text.
            timer += Time.deltaTime;
            timerUI.text = formattedTime();
        }
    }
	public int getMinutes(){
		return Mathf.FloorToInt(timer / 60F);
	}

	public int getSeconds(){
		int minutes = Mathf.FloorToInt(timer / 60F);
		return Mathf.FloorToInt(timer - minutes * 60);
	}

    private string formattedTime()
    {
		// Format time into presentable format to put in timer UI.
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        return string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    public void onFinished()
    {
		// Set 'isStarted' boolean to false when finished.
        isStarted = false;
    }
}
