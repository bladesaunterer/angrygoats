using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: A class which executes animations for any enemy based on their Animator and Animation<para/>
/// Author: Chester Booker<para/>
/// Details: Must have a functioning Animator FSM with triggers for Hit, Attack, Cast, DieTrig<para/>
/// Issues: Running animations must be done in the actual class based on the v & h inputs or speed.
/// </summary>
public static class EnemyAnimatorController
{ 
    public static void ExecuteAnimation(Animator anim, string animationName)
    {
        // Initiates Death animation and stops other animations from play or death animation from looping
        if (animationName == "Die")
        {
            anim.SetBool(animationName, true);
            anim.SetTrigger("DieTrig");
        }
        else
        {
            anim.SetTrigger(animationName);
        }
    }
}
