using UnityEngine;
using System.Collections;

public class EnemyFlash : MonoBehaviour {

	public Material flash;
	bool flag = false;

	void Awake () {
		flag = false;
	}
	public IEnumerator Flash(){
		if (flag == false) {
			flag = true;
			Renderer renderer = this.gameObject.GetComponent<Renderer>();
			if (renderer == null){
				renderer = this.gameObject.GetComponentInChildren<Renderer>();
			}
			if (renderer != null){
				Material temp = renderer.material;
				renderer.material = flash;
				yield return new WaitForSeconds (0.25f);
				renderer.material = temp;
				Debug.Log ("BOOM");
			}
			flag = false;
		}
	}
}
