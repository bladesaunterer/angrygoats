using UnityEngine;
using System.Collections;

/**
 * 
 * Class which handles special attack (bolt) logic.
 * 
 **/
public class BoltMover : MonoBehaviour
{
    
	public float speed;

	private Rigidbody rb;
	
	private Gem currentGem;
	private GameObject gemObject;

	public new Renderer renderer;
	private GemSelection gemSelection = new GemSelection ();
	
	private PlayerHealth playerHealth;

	void Start ()
	{
		currentGem = gemSelection.GetCurrentGem ();
		gemObject = GameObject.FindGameObjectWithTag (currentGem.ToString ());
		renderer = GetComponent<Renderer> ();
		// Get rigidbody and set bolt's velocity.
		rb = GetComponent<Rigidbody> ();
		rb.velocity = transform.forward * speed;

		GameObject child = this.transform.GetChild (0).gameObject;
		child.GetComponent<Renderer> ().materials = gemObject.GetComponent<Renderer> ().materials;

		if (currentGem == Gem.Green) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			Debug.Log(player.name);
			PlayerHealth playerHealth = player.gameObject.GetComponent <PlayerHealth> ();
			playerHealth.HealPlayer();
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.CompareTag ("Door") 
			|| other.gameObject.CompareTag ("Wall") 
			|| other.gameObject.CompareTag ("Enemy") 
			|| other.gameObject.CompareTag ("Boss")) {

			// Destroy bolt on contact.
			Destroy (gameObject);

			// If bolt hits an enemy, deal damage to that enemy.
			HealthControl.dealDamageToEnemy (other.gameObject);
		}
	}
	
}
