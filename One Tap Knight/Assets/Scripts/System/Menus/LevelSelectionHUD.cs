using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LevelSelectionHUD : MonoBehaviour {
    public Transform starGrid;
    public Text levelNumberText;
    public Text levelAreaText;

    public void UpdateInfo(int number, int area, bool[,] levelData)
    {
        levelNumberText.text = "Level " + number;
        levelAreaText.text = "Area " + area;
        for(int i = 0; i < World.STARS_PER_LEVEL; i++){
            starGrid.GetChild(i).GetChild(0).gameObject.SetActive(levelData[number,i]);
        }
    }
}
