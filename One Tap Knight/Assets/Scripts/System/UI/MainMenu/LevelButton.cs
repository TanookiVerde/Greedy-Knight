using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class LevelButton : MonoBehaviour {

    public TMP_Text levelTitle;
    public TMP_Text timer;
    public List<Image> ribbons;

    public void Initialize(Level level, int index)
    {
        levelTitle.text = level.title;
        timer.text = level.currentTime + "/" + level.timeRecord;
        SetRibbon(0, level.completed);
        SetRibbon(1, level.collectedAllDiamonds);
        SetRibbon(2, level.speedRunned);
        SetButtonAction(index);
    }
    private void SetRibbon(int i, bool b)
    {
        float tax = b ? 1f : 0.6f;
        ribbons[i].DOFade(tax, 0);
    }
    private void SetButtonAction(int levelIndex)
    {
        GetComponent<Button>().onClick.AddListener(
            delegate
            {
                int i = levelIndex;
                print("Opening Level of Index: " + i);
                Transition.transition.TransiteTo("TestScene");
            }
        );
    }
}
