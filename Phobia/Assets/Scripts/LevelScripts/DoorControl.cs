using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {
	
	public GameObject goalDoor;
	public GameObject ownRoom;
    public bool canSend = true;
    

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void EnterRoom() {
		ownRoom.transform.Find("Lights").gameObject.SetActive(true);
		ownRoom.GetComponent<RoomControl>().EnemiesHuntPlayer();
        ownRoom.GetComponent<RoomControl>().UpdateMinimap();
    }
	
	public void ExitRoom() {
        ownRoom.transform.Find("Lights").gameObject.SetActive(false);
		ownRoom.GetComponent<RoomControl>().EnemiesGoHome();
	}
}
