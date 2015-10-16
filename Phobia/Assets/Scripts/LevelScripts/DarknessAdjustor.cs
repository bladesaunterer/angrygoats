using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Purpose: Used for adjusting the darkness's transparency.<para/>
/// Author: Chester Booker
/// </summary>
public class DarknessAdjustor : MonoBehaviour {

    public RawImage darkness;
    public float transparencyLevel = 0;

	void Update () {

        RawImage darkImg = darkness.GetComponent<RawImage>();
        Color c = darkImg.color;
        
        // Updates the transparency level of the image based on the public input
        c.a = transparencyLevel;
        darkImg.color = c;
    }

    // 0 lighter | 1 darker
    public void setDarknessLevel(float d)
    {
        transparencyLevel = d;
    }
}


