using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionPanel : MenuPanel {

    public void OpenLevel(int levelIndex = 0)
    {
        Transition.transition.TransiteTo("TestScene");
    }
}
