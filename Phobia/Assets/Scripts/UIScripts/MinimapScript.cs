using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class MinimapScript : MonoBehaviour
{
    public CanvasGroup miniMapUI;

    public Image roomImagePrefab;
    public Image doorImagePrefab;

    public Sprite currentRoomSprite;
    public Sprite exploredRoomSprite;

    // This determines which key will popup the minimap
    private KeyCode miniMapToggleKey = KeyCode.LeftShift;
    private Dictionary<Vector2, Image> roomsDict = new Dictionary<Vector2, Image>();

    private Image currentRoomImage;

    // Update is called once per frame
    void Update()
    {
        // Show/Hide the minimap
        if (Input.GetKeyDown(miniMapToggleKey))
        {
            miniMapUI.alpha = 1f;
        }
        else if (Input.GetKeyUp(miniMapToggleKey))
        {
            miniMapUI.alpha = 0f;
        }
    }

    public void GenerateMapBlock(Vector2 location)
    {
        // Instantiate room image
        Vector3 position = new Vector3(location.x * 30, location.y * 30 - 30, 0);
        Image roomImage = Instantiate(roomImagePrefab, position, Quaternion.identity) as Image;

        // Add image to dictionary and add it to the canvas
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

    public void PlayerEntersRoom(Vector2 location, List<Vector2> doorLocations)
    {
        // Change the old room sprite back to blank
        ChangeImage(currentRoomImage, "");

        // Fetch current room image as set it as current
        currentRoomImage = roomsDict[location];
        UnlockMapBlock(currentRoomImage);
        ChangeImage(currentRoomImage, "current");

        // Generate paths between rooms
        foreach (var door in doorLocations)
        {
            Vector3 position = new Vector3(location.x * 30 + door.x * 15, location.y * 30 + door.y * 15 - 30, 0);
            Image doorImage = Instantiate(doorImagePrefab, position, Quaternion.identity) as Image;
            doorImage.transform.SetParent(miniMapUI.transform, false);
        }
    }

    void UnlockMapBlock(Image image)
    {
        image.gameObject.SetActive(true);
    }

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
