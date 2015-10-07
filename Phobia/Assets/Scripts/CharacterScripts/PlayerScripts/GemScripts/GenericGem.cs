using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenericGem : MonoBehaviour {

	protected GameObject player;
	protected PlayerControl playerControl;

	protected GameObject shot;             		// The special attack object.
	protected Transform shotSpawn;         		// Location where the special attack will spawn. 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		playerControl = player.GetComponent<PlayerControl>();
		shot = playerControl.shot;
		shotSpawn = playerControl.shotSpawn;
	}
}
