using UnityEngine;
using System.Collections;

public class BasicGem : GenericGem
{

	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.K) && playerControl.cooldown >= cost) {
			playerControl.SubtractCooldown (cost);
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);


		}
	
	}

	public override void onEnemyHit (GameObject other)
	{
		base.onEnemyHit (other);
	}
}
