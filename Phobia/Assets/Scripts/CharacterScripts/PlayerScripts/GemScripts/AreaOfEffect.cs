using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Gem ability which creates a cone of bolts. <para/>
/// Easier hitting potential but weaker damage
/// </summary>
public class AreaOfEffect : GenericGem
{
	// Override generic gem and spawn multiple bolts.
	protected override void doEffect ()
	{
		// Apply energy/mana reduction
		playerControl.SubtractCooldown (cost);

		// Spawn shots at different angles
		for (int i = 0; i < 6; i++) {
			GameObject shotSpawned = Instantiate (shot, shotSpawn.position, Quaternion.Euler (0, 37 + (shotSpawn.rotation.eulerAngles.y - 15 * i), 0)) as GameObject;
			shotSpawned.GetComponent<BoltMover> ().SetGemObject (gameObject);
		}
	}

	// Apply the base method in generic gem for appling damage to enemies
	public override void onEnemyHit (GameObject other)
	{
		base.onEnemyHit (other);
	}

	// Uses an aoe animation
	public override void castAnimation ()
	{
		anim.SetTrigger ("AOESpell");
	}
}