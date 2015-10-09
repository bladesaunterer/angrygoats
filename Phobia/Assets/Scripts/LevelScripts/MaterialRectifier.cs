using UnityEngine;
using System.Collections;

public class MaterialRectifier : MonoBehaviour {
	
	public int blockX = 2;
	public int blockZ = 2;
	// Use this for initialization
	void Start () {
		foreach (Transform childTransform in gameObject.transform) {
			Material childMaterial = childTransform.gameObject.GetComponent<Renderer>().material;
			Vector2 offset = new Vector2(-(childTransform.position.x/blockX)*childMaterial.mainTextureScale.x,
											-(childTransform.position.z/blockZ)*childMaterial.mainTextureScale.y);
			childMaterial.mainTextureOffset = offset;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}
}
