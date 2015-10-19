using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

/// <summary>
/// Purpose: Minimap generation and keyboard control.
/// 
/// Generates a hidden minimap based on the world which was randomly generated.
/// Reveals the minimaps sections more and more as the player explores different rooms.
/// Triggers the minimap to popup when a specific keyboard button is pressed. Currently: LEFT SHIFT
/// /// </summary>
public class MinimapScript : MonoBehaviour
{
    // Holds all minimap components to allow for hiding/showing 
    public CanvasGroup miniMapUI;

    // Images which will be places on the map
    public Image roomImagePrefab;
    public Image doorImagePrefab;

    // Sprites which will be swapped at run-time to indicate which room the player is currently in
    public Sprite currentRoomSprite;
    public Sprite exploredRoomSprite;

    // This determines which key will popup the minimap
    private KeyCode miniMapToggleKey = KeyCode.LeftShift;

    private Dictionary<Vector2, Image> roomsDict = new Dictionary<Vector2, Image>();
    private Image currentRoomImage;

    // Update is called once per frame
    void Update()
    {
        // Show/Hide the minimap when specific key is pressed
        if (Input.GetKeyDown(miniMapToggleKey))
        {
            miniMapUI.alpha = 1f;
        }
        else if (Input.GetKeyUp(miniMapToggleKey))
        {
            miniMapUI.alpha = 0f;
        }
    }

    // This is called by the levelGen script each time it places a room
    public void GenerateMapBlock(Vector2 location)
    {
        // Instantiate room image
        Vector3 position = new Vector3(location.x * 30, location.y * 30, 0);
        Image roomImage = Instantiate(roomImagePrefab, position, Quaternion.identity) as Image;

        // Add the image to dictionary and add it to the canvas
        roomsDict[location] = roomImage;
        roomImage.transform.SetParent(miniMapUI.transform, false);

        // All room images are hidden except for the starting room
        if (location == new Vector2(0, 0))
        {
            currentRoomImage = roomImage;
            UnlockMapBlock(roomImage);
            ChangeImage(roomImage, "current");
        }
        else
        {
            roomImage.gameObject.SetActive(false);
        }
    }

    // This is called by the doorControl script each time a player leaves a room and goes into another.
    public void PlayerEntersRoom(Vector2 location, List<Vector2> doorLocations)
    {
        // Change the old room sprite back to blank
        ChangeImage(currentRoomImage, "");

        // Fetch current room image as set it as current
        currentRoomImage = roomsDict[location];
        UnlockMapBlock(currentRoomImage);
        ChangeImage(currentRoomImage, "current");

        // Create the path images between rooms to indicate there are doors there
        foreach (var door in doorLocations)
        {
            Vector3 position = new Vector3(location.x * 30 + door.x * 15, location.y * 30 + door.y * 15, 0);
            Image doorImage = Instantiate(doorImagePrefab, position, Quaternion.Euler(0, 0, door.x * 90)) as Image;
            doorImage.transform.SetParent(miniMapUI.transform, false);
        }
    }

    // Useful method for revealing a room on the map
    void UnlockMapBlock(Image image)
    {
        image.gameObject.SetActive(true);
    }

    // Useful method for changing the room sprite
    void ChangeImage(Image image, string type)
    {
        if (type == "current")
        {
            image.sprite = currentRoomSprite;
        }
        else
        {
            image.sprite = exploredRoomSprite;
        }
    }
}
