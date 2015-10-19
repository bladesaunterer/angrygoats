using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Purpose: Handles enemy health logic.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    public AudioClip deathSound;
    public AudioClip hurtSound;

    public int startingHealth;                  // The amount of health the enemy starts the game with.
    public int currentHealth;                   // The current health of the enemy.
    public int lethalLow;

    public int scoreAwarded = 0;                // Used to keep track of the score for the score board
    public Image HealthBar;                     // Only needs to be set for the boss

    private bool isDead = false;
    private Animator anim;


    void Start()
    {
        // Get the animator component
        try
        {
            anim = GetComponent<EnemyAnimatorFinding>().getEnemyAnimator();
        }
        catch
        {
            Debug.Log("Did not find EnemyAnimatorFinding script!");
        }
    }

    void Awake()
    {
        // Set the current health when the enemy first spawns.
        currentHealth = startingHealth;
    }

    void Update()
    {
        // Only use for bosses with healthbars
        if (HealthBar != null)
        {
            float healthRatio = (float)currentHealth / (float)startingHealth;
            HealthBar.GetComponent<EnemyHealthBar>().SetHealthVisual(healthRatio);

        }
        if (gameObject.transform.position.y < lethalLow && currentHealth > 0)
        {
            TakeDamage(startingHealth);
        }
    }

    public void TakeDamage(int amount)
    {
        // Reduce current health by the amount of damage taken.
        currentHealth -= amount;

        // Trigger hurt sound if it exists
        if (hurtSound != null)
        {
            SfxScript.playSound(hurtSound);
        }

        // Trigger hurt animation if it exists
        if (anim != null)
        {
            EnemyAnimatorController.ExecuteAnimation(anim, "Hit");
        }

        // Trigger flash if it exists
        EnemyFlash flash = this.gameObject.GetComponent<EnemyFlash>();
        if (flash != null)
        {
            StartCoroutine(flash.Flash());
        }

        // If the current health is less than or equal to zero
        if (currentHealth <= 0)
        {
            if (isDead == false)
            {
                isDead = true;
                Debug.Log("Enemy Destroyed!");
                if (this.tag == "Enemy" || this.tag == "Boss")
                {
                    // Increment score when destroyed.
                    Debug.Log("INCREMEMNTING!");

                    // Increment the score
                    ScoreScript.Instance.IncrementScore(scoreAwarded);
                }

                // Play death sound if it exists
                if (deathSound != null)
                {
                    SfxScript.playSound(deathSound);
                }

                // Play death animation if it exists
                if (anim != null)
                {
                    EnemyAnimatorController.ExecuteAnimation(anim, "Die");
                }

                // Play spider aninimation exclusively
                SpiderAnimation temp = GetComponent<SpiderAnimation>();
                if (temp != null)
                {
                    Debug.Log("THIS IS A SPIDER!");
                    temp.spiderKilled();
                }
                else
                {
                    Destroy(gameObject, 1f);
                }
            }
        }
    }
}
