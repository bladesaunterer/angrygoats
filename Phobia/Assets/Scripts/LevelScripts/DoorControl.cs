using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Turning on/off the lights, triggering enemy movements and updating the minimap as the player moves from room to room.<para/>
/// </summary>
public class DoorControl : MonoBehaviour
{
    public GameObject goalDoor;
    public GameObject ownRoom;
    public bool canSend = true;

    // Turn lights in current room on, make the enemies hunt the player, update the minimap GUI
    public void EnterRoom()
    {
        ownRoom.transform.Find("Lights").gameObject.SetActive(true);
        ownRoom.GetComponent<RoomControl>().EnemiesHuntPlayer();
        ownRoom.GetComponent<RoomControl>().UpdateMinimap();
    }

    // Turn the old rooms lights off, make the enemies return to their starting positions
    public void ExitRoom()
    {
        ownRoom.transform.Find("Lights").gameObject.SetActive(false);
        ownRoom.GetComponent<RoomControl>().EnemiesGoHome();
    }
}
