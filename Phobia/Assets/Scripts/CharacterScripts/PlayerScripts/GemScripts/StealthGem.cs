using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: The stealth/purple gem. This is useful for speed runners as it causes you to be undetectable by enemies for a period of time.<para/>
/// Authors:
/// </summary>
public class StealthGem : GenericGem
{

    public bool isInvis = false;
    public GameObject oldRoom;

    private Material material;

    protected override void Update() {
        // Check if invisible
        if (isInvis)
        {
            // If the time runs out or the player conducted an attack, stop the invisible ability
            if (Time.time > endTime || Input.GetKeyDown(KeyCode.J) || (!isCurrent && Input.GetKeyDown(KeyCode.K)))
            {
                if (playerControl.currentRoom != null)
                {
                    playerControl.currentRoom.GetComponent<RoomControl>().EnemiesHuntPlayer();
                }

                // Reveal the players full model			
                Color temp = material.color;
                temp.a = 1;
                material.color = temp;

                // Turn off invisable
                isInvis = false;
            }
            else if (oldRoom != playerControl.currentRoom)
            {
                playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
                oldRoom = playerControl.currentRoom;
            }
        }
		else {
			base.Update();
		}
    }

    protected override void doEffect() {
        if (!isInvis)
        {

            // Cause the player to go invisible for a period of time
            playerControl.SubtractCooldown(cost);
            if (playerControl.currentRoom != null)
            {
                playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
            }
            endTime = Time.time + duration;

            // Reduce transparancy so just the staff is left over
            material = player.transform.Find("LowPolyNecromancer").gameObject.GetComponent<Renderer>().material;
            Color temp = material.color;
            temp.a = 0;
            material.color = temp;

            // Turn of invisable
            isInvis = true;
        }
    }
}
