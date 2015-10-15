using UnityEngine;
using System.Collections;

public class AreaOfEffect : GenericGem
{

	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.K) && playerControl.cooldown >= cost && checkActive()) {
			playerControl.SubtractCooldown (cost);
			for (int i=0; i<6; i++) {
				Instantiate (shot, shotSpawn.position, Quaternion.Euler (0, 37 + (shotSpawn.rotation.eulerAngles.y - 15 * i), 0));
			}
		

		}
	
	}

	public override void onEnemyHit (GameObject other)
	{
		base.onEnemyHit (other);
	}
}
