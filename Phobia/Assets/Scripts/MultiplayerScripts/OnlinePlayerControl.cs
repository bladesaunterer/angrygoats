using UnityEngine;
using UnityEngine.Networking;

public class OnlinePlayerControl : NetworkBehaviour {



    override public void OnStartClient()
    {
        Debug.Log("started client");
    }

    override public void OnStartLocalPlayer()
    {
        Debug.Log("started local player");
		this.gameObject.GetComponent<PlayerControl> ().setCooldownSlider ();
		this.gameObject.GetComponent<PlayerHealth> ().setHealthSlider ();
		this.gameObject.GetComponent<PlayerHealth> ().setDeadScreen ();
		GameObject.FindGameObjectWithTag ("Boss").GetComponent<EnemyHealth> ().setWonScreen ();
    }

}
