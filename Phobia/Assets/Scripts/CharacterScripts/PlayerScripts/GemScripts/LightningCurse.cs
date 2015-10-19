using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: The lightning curse applied to the enemy/boss.<para/>
/// Authors:
/// </summary>
public class LightningCurse : MonoBehaviour
{
    private float endTime;
    private AIPath path;

    // Use this for initialization
    void Start()
    {
        path = gameObject.GetComponent<AIPath>();
        path.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > endTime)
        {
            path.enabled = true;
            Destroy(this);
        }
    }

    public void updateEndTime(float time)
    {
        endTime = time;
    }
}
