using UnityEngine;
using System.Collections;

/**
 * Class which handles enemy attack logic.
 **/
public class SpiderAttack : EnemyAttack
{

    protected override void attackAnimation()
    {
        GetComponent<SpiderAnimation>().attackAnim();
    }

}

