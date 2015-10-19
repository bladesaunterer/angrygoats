using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Class for updating the UI based on gems equipped.
/// </summary>
public class GemUI : MonoBehaviour
{
	private string CRYSTAL_FOLDER = "Sprites/Crystals/"; // Folder to load crystal images from. 
	private Image gemOne;
	private Image gemTwo;

	private Sprite gemOneSprite;
	private Sprite gemTwoSprite;

	// Use this for initialization
	void Start ()
	{
		// Load images and sprites.
		Image[] images = GetComponentsInChildren<Image> ();
		foreach (Image image in images) {
			if (image.name.Equals ("GemOne")) {
				gemOne = image;
			} else if (image.name.Equals ("GemTwo")) {
				gemTwo = image;
			}
		}

		gemOneSprite = loadCrystalSprite (GemManager.Instance.GetGemOne ());
		gemOne.sprite = gemOneSprite;
		gemTwoSprite = loadCrystalSprite (GemManager.Instance.GetGemTwo ());
		gemTwo.sprite = gemTwoSprite;

	}

	// Update is called once per frame
	void Update ()
	{
		// Update UI when L is pushed.
		if (Input.GetKeyDown (KeyCode.L)) {
			// For first level, there is only one gem so don't switch.
			if (gemTwoSprite == null) {
				return;
			}

			// Else do switching if there are two gems equipped.
			if (gemOne.sprite == gemOneSprite) {
				gemOne.sprite = gemTwoSprite;
				gemTwo.sprite = gemOneSprite;
			} else {
				gemOne.sprite = gemOneSprite;
				gemTwo.sprite = gemTwoSprite;
			}
		}
	}

	// Loads a sprite from the resources folder.
	private Sprite loadCrystalSprite (Gem gem)
	{
		return Resources.Load<Sprite> (CRYSTAL_FOLDER + gem.ToString ());
	}
}
