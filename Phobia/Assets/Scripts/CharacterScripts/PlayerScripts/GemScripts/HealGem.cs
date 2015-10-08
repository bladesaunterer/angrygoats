using UnityEngine;
using System.Collections;

public class HealGem : GenericGem {

	public int heal = 10;				// How much heal this gem does
	public int cost = 25;               	// How much each special attack costs.
	
	// Update is called once per frame
	void Update () {
		//refund the cooldown
		PlayerHealth playerHealth = player.GetComponent<PlayerHealth> ();

		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost)
		{
			if (playerHealth.currentHealth != 100){
				playerControl.SubtractCooldown(cost);
				player.GetComponent<PlayerHealth>().HealPlayer(heal);
			}
		}
	}
}
