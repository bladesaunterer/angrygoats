using UnityEngine;
using System.Collections;

public class ButtonNextLevel : MonoBehaviour {

    public void NetLevelButton(int index)
    {
        Application.LoadLevel(index);
    }

    public void NextLevelButton(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}
