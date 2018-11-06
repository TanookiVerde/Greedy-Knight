using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionPanel : MenuPanel {

	public void OpenLevel(int levelIndex)
    {
        Transition.transition.TransiteTo("TestScene");
    }
}
