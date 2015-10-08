using UnityEngine;
using System.Collections;

public class BossOne : MonoBehaviour {
	float timer = 5f;
	int flag = 0;
	public GameObject one;
	public GameObject two;
	Vector3 left = new Vector3 (2, 0, 0);
	Vector3 leftl = new Vector3 (4, 0, 0);
	Vector3 right = new Vector3 (-2, 0, 0);
	Vector3 rightr = new Vector3 (-4, 0, 0);
	Vector3 up = new Vector3 (0, 0, 2);
	Vector3 down = new Vector3 (0, 0, -2);
	EnemyHealth eh;
	// Use this for initialization
	void Start () {
		eh = this.gameObject.GetComponent<EnemyHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (eh.currentHealth < eh.startingHealth) {
			if (timer > 5f) {
				timer = 0;
				if (flag == 0) {
					GameObject make = (GameObject)GameObject.Instantiate (one, this.gameObject.transform.position + left, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make = (GameObject)GameObject.Instantiate (one, this.gameObject.transform.position + right, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;

					flag = 1;
				} else {
					GameObject make = (GameObject)GameObject.Instantiate (two, this.gameObject.transform.position + up, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					make = (GameObject)GameObject.Instantiate (two, this.gameObject.transform.position + down, this.gameObject.transform.rotation);
					make.GetComponent<AIPath> ().target = GameObject.FindWithTag ("Player").transform;
					flag = 0;
				}
			}
		}
	}
}
