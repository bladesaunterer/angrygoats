using UnityEngine;
using System.Collections;

public class DarknessFollow : MonoBehaviour
{

    public RectTransform canvasRectT;
    public RectTransform darknessImage;
    public GameObject objectToFollow;

    void Update()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, objectToFollow.transform.position);
        darknessImage.anchoredPosition = screenPoint - canvasRectT.sizeDelta / 2f;
    }
}