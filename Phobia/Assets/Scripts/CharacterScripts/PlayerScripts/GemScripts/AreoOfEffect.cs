using UnityEngine;
using System.Collections;

public class AreoOfEffect : GenericGem
{

	private EnemyHealth health;
	public int damage;
	public int cost;

	// Use this for initialization
	void Start ()
	{
		player = GameObject.FindGameObjectWithTag ("Player");
		playerControl = player.GetComponent<PlayerControl> ();
		shot = playerControl.shot;
		shotSpawn = playerControl.shotSpawn;
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.K) && playerControl.cooldown >= cost) {
			playerControl.SubtractCooldown (cost);
			for (int i=0; i<6; i++) {
				Instantiate (shot, shotSpawn.position, Quaternion.Euler (0, i * 60, 0));
			}


		}
	
	}

	public override void onEnemyHit (GameObject other)
	{
		health = other.GetComponent<EnemyHealth> ();
		if (health != null) {
			health.TakeDamage (damage);
		}
	}
}
