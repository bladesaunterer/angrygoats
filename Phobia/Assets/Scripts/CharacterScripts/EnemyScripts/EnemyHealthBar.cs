using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Purpose: A health bar above bosses heads which changes from green to red as their health reduced.<para/>
/// Author: Chester Booker <para/>
/// Reference code: - See more at: http://unitydojo.blogspot.co.nz/2014/09/unity-46-simple-tutorial-health-bar-and.html#sthash.9xmPpLui.dpuf
/// </summary>
public class EnemyHealthBar : MonoBehaviour
{
    // Teating Health bar purposes
    // public float x;

    public enum SelectedAxis
    {
        xAxis,
        yAxis,
        zAxis
    }

    public SelectedAxis selectedAxis = SelectedAxis.xAxis;

    // Target
    public Image image;

    // Parameters
    public float minValue = 0.0f;
    public float maxValue = 1.0f;
    public Color minColor = Color.red;
    public Color maxColor = Color.green;

    // The default image is the one in the gameObject
    void Start()
    {
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }

    void Update()
    {
        // Health bar testing purposes
        // SetHealthVisual(x);


        switch (selectedAxis)
        {
            case SelectedAxis.xAxis:
                // Lerp color depending on the scale factor
                image.color = Color.Lerp(minColor,
                                         maxColor,
                                         Mathf.Lerp(minValue,
                                  maxValue,
                                  transform.localScale.x));
                break;
            case SelectedAxis.yAxis:
                // Lerp color depending on the scale factor
                image.color = Color.Lerp(minColor,
                                         maxColor,
                                         Mathf.Lerp(minValue,
                                  maxValue, transform.localScale.y));
                break;
            case SelectedAxis.zAxis:
                // Lerp color depending on the scale factor
                image.color = Color.Lerp(minColor,
                                         maxColor,
                                         Mathf.Lerp(minValue,
                                  maxValue,
                                  transform.localScale.z));
                break;
        }
    }

    // Health between [0.0f,1.0f] == (currentHealth / totalHealth)
    public void SetHealthVisual(float healthNormalized)
    {
        if (healthNormalized > 0f)
        {
            transform.localScale = new Vector3(healthNormalized,
                                                     transform.localScale.y,
                                                     transform.localScale.z);
        }
        transform.localScale = new Vector3(0f,
                                                     transform.localScale.y,
                                                     transform.localScale.z);
    }
}

