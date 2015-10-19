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
        
        if (isInvis) {
            // If the invis time runs out or the player execute an attack, or casted another spell, stop the invisibility ability
            if (Time.time > endTime || Input.GetKeyDown(KeyCode.J) || (!isCurrent && Input.GetKeyDown(KeyCode.K))) {
                if (playerControl.currentRoom != null) {
                    playerControl.currentRoom.GetComponent<RoomControl>().EnemiesHuntPlayer();
                }

                // Make the player visible			
                Color temp = material.color;
                temp.a = 1;
                material.color = temp;

                // Turn off invisible
                isInvis = false;
            }
            else if (oldRoom != playerControl.currentRoom) { // When the player enters a new room, tell the enemies not to attack
                playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
                oldRoom = playerControl.currentRoom;
            }
        }
		else { // superclass handles the casting of the spell
			base.Update();
		}
    }

    protected override void doEffect() {
        if (!isInvis) {

            // Cause the player to go invisible for a period of time
            playerControl.SubtractCooldown(cost);
            if (playerControl.currentRoom != null) {
                playerControl.currentRoom.GetComponent<RoomControl>().EnemiesGoHome();
            }
            endTime = Time.time + duration;

            // Reduce transparency so only the staff is visible
			// Staff is a different material, so we don't explicitly set its visibility
            material = player.transform.Find("LowPolyNecromancer.001").gameObject.GetComponent<Renderer>().material;
            Color temp = material.color;
            temp.a = 0;
            material.color = temp;

            isInvis = true;
        }
    }

    // Overrides with a heal animation
    public override void castAnimation() {
        anim.SetTrigger("AOESpell");
    }
}
