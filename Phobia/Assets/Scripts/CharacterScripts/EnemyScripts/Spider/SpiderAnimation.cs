using UnityEngine;
using System.Collections;

public class SpiderAnimation : MonoBehaviour {

    public Animation animations;
    public string attack = "attack1";
    public string move = "walk";
    public string death = "death2";

    // Setup animations
    void Start()
    {
        // Set all animations to loop
        animations.wrapMode = WrapMode.Loop;
        // except attacking and death
        animations[attack].wrapMode = WrapMode.Once;
        animations[death].wrapMode = WrapMode.Once;

        // Attacking takes priority over moving. Dying takes highest priority.
        animations[attack].layer = 1;
        animations[death].layer = 2;
    }

    // Update is called once per frame
    void Update () {
        // TODO if moving (otherwise no animation)
        animations.CrossFade(move);
    }

    public void attackAnim ()
    {
        animations.CrossFade(attack);
    }

    public void spiderKilled()
    {
        animations.Play(death);
        // Stop moving
        GetComponent<AIPath>().speed = 0;
        // Destroy after death animation has finished
        Destroy(gameObject, animations[death].length);
    }
}
