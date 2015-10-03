using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    Text timerUI;
    float timer = 0;
    public static bool isStarted;

    void Start()
    {
        timerUI = GetComponent<UnityEngine.UI.Text>();
        isStarted = true;
    }

    void Update()
    {
        if (isStarted)
        {
            timer += Time.deltaTime;
            timerUI.text = formattedTime();
        }
    }

    private string formattedTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        return string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    public void onFinished()
    {
        isStarted = false;
    }
}
