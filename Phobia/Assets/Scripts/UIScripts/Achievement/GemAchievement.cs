using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GemAchievement : MonoBehaviour {
	
	public string name;

	[TextArea(3,10)]
	public string lockeddescription;
	[TextArea(3,10)]
	public string unlockeddescription;

	public Sprite lockedimage;
	public Sprite unlockedimage;
	
	private GemManager gemManager = new GemManager();

	public Gem achievement;

	private bool unlocked;
	private Image image;
	private Text title;
	private Text description;

	// Use this for initialization
	void Start () {
		unlocked = gemManager.CheckIfGemUnlocked(achievement);
		foreach (Transform child in transform){
			if(child.gameObject.name == "Image"){
				image = child.gameObject.GetComponent<Image>();
			}
			if(child.gameObject.name == "Name"){
				title = child.gameObject.GetComponent<Text>();
			}
			if(child.gameObject.name == "Description"){
				description = child.gameObject.GetComponent<Text>();
			}
		}
		title.text = name;
		if (unlocked){
			image.sprite = unlockedimage;
			description.text = unlockeddescription;
		} else {
			image.sprite = lockedimage;
			description.text = lockeddescription;
		}
	}

}
