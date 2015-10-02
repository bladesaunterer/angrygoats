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
    }
}
