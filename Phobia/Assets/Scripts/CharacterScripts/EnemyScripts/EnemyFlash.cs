using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: A script to attact to enemies to visually signify that they have taken damage.<para/>
/// </summary>
public class EnemyFlash : MonoBehaviour
{

    public Material flash;
    bool flag = false;

    void Awake()
    {
        flag = false;
    }

    public IEnumerator Flash()
    {
        if (flag == false)
        {
            flag = true;

            // Find renderer reference
            Renderer renderer = this.gameObject.GetComponent<Renderer>();
            if (renderer == null)
            {
                renderer = this.gameObject.GetComponentInChildren<Renderer>();
            }


            if (renderer != null)
            {
                // Change the material of the enemy for a short period of time, then revert it back
                Material temp = renderer.material;
                renderer.material = flash;
                yield return new WaitForSeconds(0.25f);
                renderer.material = temp;
            }
            flag = false;
        }
    }
}
