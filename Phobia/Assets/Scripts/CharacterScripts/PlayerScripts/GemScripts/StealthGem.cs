using UnityEngine;
using System.Collections;

public class StealthGem : GenericGem {
	
	public bool isInvis = false;
	public GameObject oldRoom;
	
	private Material material;
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost && isCurrent) {
			if (!isInvis) {
				playerControl.SubtractCooldown(cost);
				
				if (playerControl.currentRoom != null) {
					playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
				}
				endTime = Time.time + duration;
				
				material = player.transform.Find("LowPolyNecromancer").gameObject.GetComponent<Renderer>().material;
				Color temp = material.color;
				temp.a = 0;
				material.color = temp;
				
				isInvis = true;
			}
		}
		
		if (isInvis) {
			if (Time.time > endTime || Input.GetKeyDown(KeyCode.J) || (!isCurrent && Input.GetKeyDown(KeyCode.K))) {
			
				if (playerControl.currentRoom != null) {
					playerControl.currentRoom.GetComponent<RoomControl>().EnemiesHuntPlayer();
				}
				
				Color temp = material.color;
				temp.a = 1;
				material.color = temp;
				
				isInvis = false;
			}
			else if (oldRoom != playerControl.currentRoom) {
				playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
				oldRoom = playerControl.currentRoom;
			}
		}
	}
}
