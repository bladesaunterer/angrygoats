using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Used for the Darkness level to make the dark image follow the player.<para/>
/// Author: Chester Booker<para/>
/// Issue: The size of the "vision" does not scale well with the screen size.
/// </summary>
public class DarknessFollow : MonoBehaviour
{
    public RectTransform darknessImage;
    public Transform objectToFollow;

    void Update()
    {
        // Used for testing the mouse to control the darkness.
        //Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        // Converts the 3D player to 2D pixel locations on the screen
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(objectToFollow.position);

        // Determines the offset of the darkness image by examining the screen's dimensions
        Vector2 imageOffset = new Vector2(Screen.width/2, Screen.height / 2);

        // Sets the darkness image based on the players position - the screen's dimensions
        darknessImage.anchoredPosition = screenPoint - imageOffset;
    }
}