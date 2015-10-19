using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Purpose: Used for adjusting the darkness's transparency.<para/>
/// </summary>
public class DarknessAdjustor : MonoBehaviour
{
    public RawImage darkness;
    public float transparencyLevel = 0;

    private RawImage darkImg;

    void Start()
    {
        // Fetch the dark overlay
        darkImg = darkness.GetComponent<RawImage>();
    }
    void Update()
    {

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


