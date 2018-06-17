using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class World : MonoBehaviour {
    public const int LEVEL_QUANTITY = 9;
    public const int STARS_PER_LEVEL = 3;
    private const string SAVE_NAME = "save";
    private const string LOAD_ERROR = "error";
    private const string FINISHED_LEVEL = "finished";
    private const string SELECTED_LEVEL_NAME = "player";

    public bool[,] levelProgression = new bool[LEVEL_QUANTITY, STARS_PER_LEVEL];
    public Level[] allLevels = new Level[LEVEL_QUANTITY];
    public LineRenderer progressionLine;
    public Transform levelSelector;
    public float selectorPositionDuration;

    public static int lastUnlockedLevel;
    public static int selectedLevel;

    public LevelSelectionHUD hud;
    public LevelSelectionCamera cam;

    private void Start()
    {
        LoadProgression();
        if(WasOpenedAfterFinishedLevel()){
            FinishLevel(PlayerPrefs.GetString(FINISHED_LEVEL,LOAD_ERROR));
        }
        UpdateLineProgression();
        //PARTE DE TESTES
        LevelFinishData lfd = new LevelFinishData();
        lfd.completed = true;
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        FinishLevel(JsonUtility.ToJson(lfd));
        //FIM PARTE DE TESTES
        UpdateSelectorPosition(0,true);
        cam.MoveCamera();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UpdateSelectorPosition(-1);
        } else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            UpdateSelectorPosition(+1);
        }
    }
    private void GetCurrentLevel()
    {
        World.lastUnlockedLevel = 0;
        for (int i = 0; i < LEVEL_QUANTITY; i++)
        {
            if (!levelProgression[i, 0])
            {
                World.lastUnlockedLevel = i;
                break;
            }
        }
        print(World.lastUnlockedLevel);
    }
    private void UpdateLineProgression()
    {
        progressionLine.SetPosition(1,allLevels[lastUnlockedLevel].GetPointPosition());
    }
    private void UnlockNextLevel()
    {
        if(lastUnlockedLevel == LEVEL_QUANTITY-1) return;
        lastUnlockedLevel++;
        Sequence s = DOTween.Sequence();
        s.Append( DOProgressLine(allLevels[lastUnlockedLevel].GetPointPosition(),1f));
        s.Append( allLevels[lastUnlockedLevel].GrowAnimation());
        s.Append( allLevels[lastUnlockedLevel].ShortenAnimation());
    }
    private bool WasOpenedAfterFinishedLevel()
    {
        return PlayerPrefs.GetInt(FINISHED_LEVEL, 0) > 0;
    }
    private void FinishLevel(string jsonData)
    {
        if(jsonData == LOAD_ERROR) return;
        LevelFinishData data = JsonUtility.FromJson<LevelFinishData>(jsonData);
        if (data.completed)
        {
            levelProgression[selectedLevel, 0] = data.completed;
            if (data.gotAllCoins) levelProgression[selectedLevel, 1] = true;
            if (data.gotNoCoins) levelProgression[selectedLevel, 2] = true;
            UnlockNextLevel();
        }
    }
    private void SaveProgression()
    {
        string jsonData = JsonUtility.ToJson(levelProgression);
        PlayerPrefs.SetString(SAVE_NAME, jsonData);
        PlayerPrefs.SetInt(SELECTED_LEVEL_NAME, World.selectedLevel);
    }
    private void LoadProgression()
    {
        World.selectedLevel = PlayerPrefs.GetInt(SELECTED_LEVEL_NAME, 0);
        string jsonData = PlayerPrefs.GetString(SAVE_NAME, LOAD_ERROR);
        if(jsonData != LOAD_ERROR)
            levelProgression = JsonUtility.FromJson<bool[,]>(jsonData);
        print(jsonData);
    }
    private Tweener DOProgressLine(Vector3 finalPosition,float duration)
    {
         return DOTween.To(
            () => progressionLine.GetPosition(1),
            x => progressionLine.SetPosition(1,x),
            finalPosition,
            duration);
    }
    private void UpdateSelectorPosition(int direction, bool instant = false)
    {
        if(selectedLevel + direction > lastUnlockedLevel || selectedLevel + direction < 0) return;
        selectedLevel += direction;
        float duration = instant ? 0 : selectorPositionDuration;
        levelSelector.DOMoveX(allLevels[selectedLevel].GetPointPosition().x, duration);
        hud.UpdateInfo(selectedLevel, selectedLevel/3 + 1, levelProgression);
        cam.MoveCamera();
    }
}
[System.Serializable]
public struct LevelFinishData{
    public bool completed;
    public bool gotAllCoins;
    public bool gotNoCoins;
}
