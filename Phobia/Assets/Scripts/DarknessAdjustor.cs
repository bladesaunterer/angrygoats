using UnityEngine;
using UnityEngine.UI;


using System.Collections;

public class DarknessAdjustor : MonoBehaviour {

    public RawImage darkness;
    public float transparencyLevel = 0;

	// Update is called once per frame
	void Update () {

        // Might have to change this to a public field where you link the darkness image.
        RawImage darkImg = darkness.GetComponent<RawImage>();
        Color c = darkImg.color;
   
        c.a = transparencyLevel;
        darkImg.color = c;
    }

    // 0 lighter | 1 darker
    public void setDarknessLevel(float d)
    {
        transparencyLevel = d;
    }
}


