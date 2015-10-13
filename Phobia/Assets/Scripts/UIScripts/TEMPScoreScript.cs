using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * 
 * Singleton class which handles scoring logic on UI.
 * 
 **/
public class TEMPScoreScript : MonoBehaviour
{

    private static TEMPScoreScript instance;
    public static int pointsCounter;
	public static int enemyCounter;
    public Text currentScore;

	// Singleton method for getting instance of TEMPScoreScript.
    public static TEMPScoreScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TEMPScoreScript>();
            }
            return TEMPScoreScript.instance;
        }

    }


    void Start()
    {
		pointsCounter = 0;
		enemyCounter = 0;
		SetCountText(pointsCounter, currentScore);
    }

    void Awake()
    {
        //DontDestroyOnLoad(this);
    }


    /*
    On the enemy behaviour call "OnDestroy"

    In that method, check the tag of the enemy:
    ie - small, medium, big, boss

    Then call IncrementScore(int) with the appropriate
     amount of points for said enemy.
    */

	public int GetScore(){
		return pointsCounter;
	}

	public int GetEnemies(){
		return enemyCounter;
	}


    public void IncrementScore(int points)
    {
		// Increments score and sets text on UI.
        pointsCounter += points;
		enemyCounter++;
        SetCountText(pointsCounter, currentScore);
    }

    // Method which updates the player's score on the UI.
    public void SetCountText(int value, Text gText)
    {
        if (value < 10)
        {
            gText.text = "000" + value.ToString();
        }
        else if (value < 100)
        {
            gText.text = "00" + value.ToString();
        }
        else if (value < 1000)
        {
            gText.text = "0" + value.ToString();
        }
        else
        {
            gText.text = value.ToString();
        }
    }
}
