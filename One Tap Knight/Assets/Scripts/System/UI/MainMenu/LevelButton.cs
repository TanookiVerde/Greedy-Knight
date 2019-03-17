using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LevelButton : MonoBehaviour {
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text diamondsCount;
    [SerializeField] private Image levelImage;

    private Level level;

    public void SetInfo(Level level, Sprite image)
    {
        this.level = level;
        levelImage.sprite = image;
        title.text = level.title;
        diamondsCount.text = level.completed ? level.diamondsCollected + "/36" : "-/36";
    }
    public void OpenLevel()
    {
        Transition.transition.TransiteTo("Level " + level.number);
        PlayerPrefs.SetString("levelName", "Level " + level.number);
        PlayerPrefs.SetInt("levelNumber", level.number - 1);
    }
}
