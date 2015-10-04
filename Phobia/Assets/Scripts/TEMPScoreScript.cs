using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TEMPScoreScript : MonoBehaviour
{

    private static TEMPScoreScript instance;
    public static int pointsCounter;
    public Text currentScore;

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
        SetCountText(pointsCounter, currentScore);
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }


    /*
    On the enemy behaviour call "OnDestroy"

    In that method, check the tag of the enemy:
    ie - small, medium, big, boss

    Then call IncrementScore(int) with the appropriate
     amount of points for said enemy.
    */


    public void IncrementScore(int points)
    {
        pointsCounter += points;
        SetCountText(pointsCounter, currentScore);
    }

    // Updates the players score
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
