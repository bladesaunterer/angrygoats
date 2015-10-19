using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: This handles the special attack(bolt) logic.<para/>
/// Authors:
/// 
/// </summary>
public class BoltMover : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private Gem currentGem;
    private GameObject gemObject;

    private PlayerHealth playerHealth;

    void Start() {

        // Get rigidbody and set bolt's velocity.
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;

        GameObject child = this.transform.GetChild(0).gameObject;
        child.GetComponent<Renderer>().material = gemObject.GetComponent<GenericGem>().boltMaterial;
    }

    void OnTriggerEnter(Collider other) {
        // If the bolt hits an enemy or boss
        if (other.gameObject.CompareTag("Enemy")
            || other.gameObject.CompareTag("Boss"))
        {
            // Destroy bolt on contact.
            Destroy(gameObject);

            // Deal damage to that enemy/boss
            GenericGem genericGem = gemObject.GetComponent<GenericGem>();
            genericGem.onEnemyHit(other.gameObject);
        }

        // If the bolt hits a door or wall
        if (other.gameObject.CompareTag("Door")
            || other.gameObject.CompareTag("Wall"))
        {
            // Destroy bolt on contact.
            Destroy(gameObject);
        }
    }

    public void SetGemObject(GameObject gemObj) {
        gemObject = gemObj;
    }
}
