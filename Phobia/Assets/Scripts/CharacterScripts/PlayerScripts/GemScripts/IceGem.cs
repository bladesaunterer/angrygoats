using UnityEngine;
using System.Collections;

public class IceGem : GenericGem {

	public float factor = 0.5f;


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K) && playerControl.cooldown >= cost && isCurrent)
		{
			playerControl.SubtractCooldown(cost);
			GameObject shotSpawned = Instantiate(shot,shotSpawn.position,shotSpawn.rotation) as GameObject;
			shotSpawned.GetComponent<BoltMover>().SetGemObject(gameObject);

		}
	}

	public override void onEnemyHit(GameObject other){
		base.onEnemyHit(other);
		IceCurse curse = other.GetComponent<IceCurse>();
		if (curse == null){
			curse = other.AddComponent<IceCurse>();
			curse.addFactor(factor);
		}
		curse.updateEndTime(endTime);
	}
}
