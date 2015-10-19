using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GenericAchievement : MonoBehaviour {

	public string achievementName;
	
	[TextArea(3,10)]
	public string lockeddescription;
	[TextArea(3,10)]
	public string unlockeddescription;
	
	public Sprite lockedimage;
	public Sprite unlockedimage;

	public bool secret;
	
	protected bool unlocked;
	protected Image image;
	protected Text title;
	protected Text description;

	/**
	 * This method should only be called for Other Achievements
	 */
	protected void checkUnlocked(){
		if (PlayerPrefs.GetInt(achievementName)== 0){
			unlocked = false;
		} else {
			unlocked = true;
		}
	}

	protected void UpdateUI(){
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
		if (secret){
			title.text = "Secret";
		} else {
			title.text = achievementName;
		}
		if (unlocked){
			image.sprite = unlockedimage;
			description.text = unlockeddescription;
			title.text = achievementName;
		} else {
			image.sprite = lockedimage;
			description.text = lockeddescription;
		}
	}
}
