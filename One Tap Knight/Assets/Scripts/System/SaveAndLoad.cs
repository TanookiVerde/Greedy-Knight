using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour {
	private const string SAVE_NAME = "levelData";
	private const string FINISHED_LEVEL = "finishedLevel";
	private const string ERROR = "error";
    private const string LAST_OPENED_LEVEL = "lastLevel";
    private const string LAST_OPENED_LEVEL_NAME = "lastLevelName";

    public static SaveData LoadLevelData()
	{
		string s = PlayerPrefs.GetString(SAVE_NAME, ERROR);
        if(s.Length <= ERROR.Length)
            return GetSampleData();
        else
		    return JsonUtility.FromJson<SaveData>(s);
	}
	public static void SaveLevelData(SaveData levelData)
	{
		PlayerPrefs.SetString(SAVE_NAME,JsonUtility.ToJson(levelData));
	}
	public static bool GetFinishedLevel()
	{
		int finished = PlayerPrefs.GetInt(FINISHED_LEVEL,0);
		return finished == 1;
	}
	public static void SetFinishedLevel(bool finished)
	{
		int i = finished ? 1 : 0;
		PlayerPrefs.SetInt(FINISHED_LEVEL,i);
	}
    public static int GetLastOpenedLevelIndex()
    {
        return PlayerPrefs.GetInt(LAST_OPENED_LEVEL, 0);
    }
    public static void SetLastOpenedLevel(int levelIndex)
    {
        PlayerPrefs.SetInt(LAST_OPENED_LEVEL, levelIndex);
    }
    public static string GetLastOpenedLevelName()
    {
        return PlayerPrefs.GetString(LAST_OPENED_LEVEL_NAME, "MainScene");
    }
    public static void SetLastOpenedLevelName(string levelName)
    {
        PlayerPrefs.SetString(LAST_OPENED_LEVEL_NAME, levelName);
    }
    private static SaveData GetSampleData()
    {
        SaveData s = new SaveData();
        s.levelCompleted[0] = 1;
        return s;
    }
    public static void FinishAndSaveLevel(bool allCoins, bool noCoins)
    {
        int index = SaveAndLoad.GetLastOpenedLevelIndex();
        SaveData save = SaveAndLoad.LoadLevelData();
        //save.levelCompleted[index] = 1;
        //save.allCoins[index] = allCoins ? 1 : 0;
        //save.noCoins[index] = noCoins ? 1 : 0;
        SaveAndLoad.SaveLevelData(save);
        SaveAndLoad.SetFinishedLevel(true);
        print("SAVED SUCESSFULLY");
    }
    public static void ResetSave()
    {
        SaveData s = LoadLevelData();
        s = new SaveData();
        SaveLevelData(s);
    }
}
[System.Serializable]
public class SaveData
{
    private const int LEVEL_QUANTITY = 9;

    public int[] levelCompleted;
    public int[] allCoins;
    public int[] noCoins;

    public SaveData()
    {
        levelCompleted = new int[LEVEL_QUANTITY];
        allCoins = new int[LEVEL_QUANTITY];
        noCoins = new int[LEVEL_QUANTITY];
    }
}
