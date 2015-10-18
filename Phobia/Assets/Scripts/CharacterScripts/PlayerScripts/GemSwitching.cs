using UnityEngine;
using System.Collections;

/// <summary>
/// Purpose: Handles all logic behind each gem in the game.<para/>
/// Authors:                <para/>
/// Additionally, Notifies gem manager of changes in gem selections
/// </summary>
public class GemSwitching : MonoBehaviour
{
    public GameObject gemOne;
    public GameObject gemTwo;

    private GameObject spawn;
    private Gem currentGem;
    private GemManager gemManager = GemManager.Instance;

    // Called when script is loaded
    void Awake()
    {
        // Checks to make sure gems are persisted 
        Debug.Log(gemManager.GetGemOne().ToString() + " successfully persisted");
        Debug.Log(gemManager.GetGemTwo().ToString() + " successfully persisted");

        spawn = GameObject.FindGameObjectWithTag("SpecialAttack");

        // This way broke when people added the gem switching in the hud
        // gemOne = GameObject.FindGameObjectsWithTag (gemManager.GetGemOne ().ToString ());
        // gemTwo = GameObject.FindGameObjectsWithTag (gemManager.GetGemTwo ().ToString ());

        foreach (Transform childTransform in gameObject.transform.Find("Shot Spawn"))
        {
            if (childTransform.gameObject.CompareTag(gemManager.GetGemOne().ToString()))
            {
                gemOne = childTransform.gameObject;
            }
            if (childTransform.gameObject.CompareTag(gemManager.GetGemTwo().ToString()))
            {
                gemTwo = childTransform.gameObject;
            }
        }

        // Sets all gem game objects to be inactive except selected gems
        foreach (Transform child in spawn.transform)
        {
            child.gameObject.SetActive(false);
        }
        gemOne.SetActive(true);
        gemTwo.SetActive(true);
        gemTwo.GetComponent<GenericGem>().isCurrent = false;

        // Current selection starts with gemOne
        gemManager.SetCurrentGem(gemManager.GetGemOne());

        // Updates which gem's abilities the staff is using
        updateStaff(gemOne);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangeGem();
        }
    }

    void ChangeGem()
    {
        gemOne.GetComponent<GenericGem>().isCurrent = !gemOne.GetComponent<GenericGem>().isCurrent;
        gemTwo.GetComponent<GenericGem>().isCurrent = !gemTwo.GetComponent<GenericGem>().isCurrent;

        GameObject currentGem = (gemOne.GetComponent<GenericGem>().isCurrent) ? gemOne : gemTwo;

        gemManager.SetCurrentGem(gemManager.GetEnum(currentGem.tag));
        updateStaff(currentGem);
    }

    /*
    * Update the staff the player holds to match the equipped gem, also update the smoke/particles to match.
    */
    void updateStaff(GameObject gem)
    {
        GameObject.FindGameObjectWithTag("Staff").GetComponent<Renderer>().material = gem.GetComponent<GenericGem>().staffMaterial;
        GameObject.FindGameObjectWithTag("Staff").transform.Find("smoke").GetComponent<Renderer>().material = gem.GetComponent<GenericGem>().staffParticles;
    }
}