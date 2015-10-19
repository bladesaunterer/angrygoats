using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Any object using this will automatically cause it to face the camera.<para/>
/// Potential: Is useful for health bars and other 2D movable images/object.<para/>
/// Issue: Currently not used.
/// </summary>
public class CameraFacingObject : MonoBehaviour
{

	void Update ()
	{
		// Transforms the object to face the direction of the main camera.
		transform.LookAt (transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
	}
}
