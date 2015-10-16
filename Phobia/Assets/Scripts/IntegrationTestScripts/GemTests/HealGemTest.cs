using UnityEngine;
using System.Collections;

/*
 * This tests if the heal gem works properly
 * Attach to player
 */
public class HealGemTest : MonoBehaviour {

	private HealGem gem;
	private PlayerControl player;
	private PlayerHealth health;

	private bool check;
	private float nextTime;
	private int seconds;

	private int healAmount;

	private int prevHealth;
	private int newHealth;

	private int prevCoolDown;
	private int newCoolDown;

	void Start () {
		gem = GameObject.FindGameObjectWithTag ("Green").GetComponent<HealGem>();
		player = GetComponent<PlayerControl>();
		health = GetComponent<PlayerHealth>();
		healAmount = gem.heal;
		seconds = 0;
		check = false;
	}
	

	void Update () {
		if (Time.time > nextTime){
			seconds++;
			nextTime++;
			check = true;
		}

		//Take damage after 1 second
		if (seconds == 1 && check){
			health.TakeDamage(healAmount * 2);
		}
		//Heal 
		if (seconds == 2 && check){
			prevHealth = health.currentHealth;
			health.HealPlayer(healAmount);
			newHealth = health.currentHealth;
			if(prevHealth + healAmount != newHealth){
				IntegrationTest.Fail();
			}
		}
		if (seconds == 3 && check){
			health.HealPlayer(healAmount);
			newHealth = health.currentHealth;
			if (newHealth != health.startingHealth){
				IntegrationTest.Fail();
			}
		}
		if (seconds == 4 && check){
			prevCoolDown = player.cooldown;
			health.HealPlayer(healAmount);
			newCoolDown = player.cooldown;
			if (prevCoolDown != newCoolDown){
				IntegrationTest.Fail();
			}
		}

		if (seconds == 5 && check){
			IntegrationTest.Pass();
		}
		check = false;
	}
}
