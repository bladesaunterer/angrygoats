using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Execute the spiders attack animation.<para/>
/// Authors:
/// </summary>
public class SpiderAttack : EnemyAttack
{
    protected override void attackAnimation()
    {
        GetComponent<SpiderAnimation>().attackAnim();
    }
}

