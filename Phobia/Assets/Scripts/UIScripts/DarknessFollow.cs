using UnityEngine;
using System.Collections;

public class DarknessFollow : MonoBehaviour
{

    public GameObject player;
    public GameObject dark;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - player.transform.position;
        //offset = dark.transform.position - player.transform.position;

    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        //dark.transform.position = player.transform.position + offset;
    }
}