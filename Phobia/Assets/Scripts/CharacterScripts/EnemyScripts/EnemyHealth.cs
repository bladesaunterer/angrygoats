using UnityEngine;

/**
 * 
 * Class which handles enemy health logic.
 * 
 **/
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;            // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health the enemy has.
    public int lethalLow;
	public int scoreAwarded = 0;

    public GameObject wonScreen;
	private bool isShowingWon = false;
	private bool isDed = false;

    void Awake()
    {
        // Set the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (gameObject.transform.position.y < lethalLow)
        {
            TakeDamage(startingHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        // Reduce current health by the amount of damage taken.
        currentHealth -= amount;

        // If the current health is less than or equal to zero...
        if (currentHealth <= 0) {
			if (isDed == false) {
				isDed = true;
				Debug.Log ("Enemy Destroyed!");
				if (this.tag == "Enemy" || this.tag == "Boss") {
					// Increment score when destroyed.
					Debug.Log ("INCREMEMNTING!");
					TEMPScoreScript.Instance.IncrementScore (scoreAwarded);
				}
				// ... the enemy is destroyed.
				if (gameObject.CompareTag ("Boss")) {
					isShowingWon = !isShowingWon;
					wonScreen.SetActive (isShowingWon);
					int temp1 = TEMPScoreScript.Instance.GetScore();
					int temp2 = TEMPScoreScript.Instance.GetEnemies();
					GameObject time = GameObject.Find("Timer");
					int temp3 = time.GetComponent<Timer>().getMinutes();
					int temp4 = time.GetComponent<Timer>().getSeconds();
					wonScreen.GetComponent<WinUpdate>().SetFinal(temp1,temp2,temp3,temp4);
				}

				SpiderAnimation temp = GetComponent<SpiderAnimation> ();
				if (temp != null) {
					Debug.Log ("THIS IS A SPIDER!");
					temp.spiderKilled ();
				} else {
					Destroy (gameObject);
				}

			}
		}
    }
}
