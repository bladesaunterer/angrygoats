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

	private GemManager gemManager = GemManager.Instance;
	
	private PlayerHealth playerHealth;

	void Start()
	{
		currentGem = gemManager.GetCurrentGem();

		// Get rigidbody and set bolt's velocity.
		rb = GetComponent<Rigidbody>();
		rb.velocity = transform.forward * speed;

		GameObject child = this.transform.GetChild(0).gameObject;
		child.GetComponent<Renderer>().material = gemObject.GetComponent<GenericGem>().boltMaterial;

	}

	void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.CompareTag ("Enemy") 
		    || other.gameObject.CompareTag ("Boss")){
			// Destroy bolt on contact.
			Destroy (gameObject);
			// If bolt hits an enemy, deal damage to that enemy.
			GenericGem genericGem = gemObject.GetComponent<GenericGem> ();
			genericGem.onEnemyHit (other.gameObject);
		}


		if (other.gameObject.CompareTag ("Door") 
			|| other.gameObject.CompareTag ("Wall") ) {
			// Destroy bolt on contact.
			Destroy (gameObject);
		}
	}
	public void SetGemObject(GameObject gemObj) {
		gemObject = gemObj;
	}

}
