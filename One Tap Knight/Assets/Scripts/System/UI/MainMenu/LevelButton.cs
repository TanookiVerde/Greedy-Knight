using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButton : MonoBehaviour {
    public string levelName;

    public void Open()
    {
        PlayerPrefs.SetString("levelName", levelName);
        Transition.transition.TransiteTo(levelName);
    }
}
