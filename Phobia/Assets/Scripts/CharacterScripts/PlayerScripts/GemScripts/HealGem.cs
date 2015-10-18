using UnityEngine;
using System.Collections;

public class HealGem : GenericGem {

	public int heal = 10;				// How much heal this gem does

	protected override void doEffect(){
		PlayerHealth playerHealth = player.GetComponent<PlayerHealth> ();
		if (playerHealth.currentHealth != 100){
			playerControl.SubtractCooldown(cost);
			player.GetComponent<PlayerHealth>().HealPlayer(heal);
		}
	}
}
