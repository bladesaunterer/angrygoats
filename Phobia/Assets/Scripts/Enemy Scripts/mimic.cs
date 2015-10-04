using UnityEngine;
using System.Collections;

public class mimic : MonoBehaviour
{

    public Transform target;

    public void Start()
    {
        
    }


    public void Update()
    {

        Vector3 newPos = target.transform.position;
        newPos.y = transform.position.y;
        transform.position = newPos;
        transform.rotation = target.transform.rotation;
    }
}