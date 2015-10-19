using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: A helper class which you bind to the main prefab in order to find the Animator location.<para/>
/// </summary>
public class EnemyAnimatorFinding : MonoBehaviour 
{
    // Must assign to prefab
    public Animator anim;
	
    // Used by other scripts to get the animator
    public Animator getEnemyAnimator()
    {
        return anim;
    }
}
