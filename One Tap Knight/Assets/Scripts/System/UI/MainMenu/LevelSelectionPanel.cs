using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelectionPanel : MenuPanel {

    public void OpenLevel(int levelIndex = 0)
    {
        FindObjectOfType<AudioSource>().DOFade(0, 0.5f);
        Transition.transition.TransiteTo("TestScene");
    }
}
