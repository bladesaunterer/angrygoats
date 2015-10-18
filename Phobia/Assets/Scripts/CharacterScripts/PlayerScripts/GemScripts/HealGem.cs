using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: The green/healing gem. Useful for healing the character when they are in danger and are close to dying.<para/>
/// Authors:
/// </summary>
public class HealGem : GenericGem
{
    // The amount the gem will heal for
    public int heal = 10;

    protected override void doEffect()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth.currentHealth != 100)
        {
            // Apply healing effect to player's health
            playerControl.SubtractCooldown(cost);
            player.GetComponent<PlayerHealth>().HealPlayer(heal);
        }
    }
}
