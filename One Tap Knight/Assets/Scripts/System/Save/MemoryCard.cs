using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class MemoryCard : MonoBehaviour {
    private const string SAVE_PATH = "/save.txt";

	public static AdventureLog Load()
    {
        CreateFileIfDontExist(GetSavePath(), JsonUtility.ToJson(new AdventureLog()));
        print(File.ReadAllText(GetSavePath()));
        return JsonUtility.FromJson<AdventureLog>(File.ReadAllText(GetSavePath()));
    }
    public static void Save(AdventureLog log)
    {
        CreateFileIfDontExist(GetSavePath(), JsonUtility.ToJson(log));
        print(JsonUtility.ToJson(log));
        File.WriteAllText(GetSavePath(), JsonUtility.ToJson(log));
    }
    public static string GetSavePath()
    {
        return Application.persistentDataPath + SAVE_PATH;
    }
    public static void CreateFileIfDontExist(string path, string content)
    {
        if (!File.Exists(path))
        {
            Debug.Log(path);
            File.WriteAllText(path, content);
        }
    }
}
[System.Serializable]
public class AdventureLog
{
    private const int LEVEL_AMOUNT = 5;

    public int selectedSkin;
    public int selectedTrail;
    public List<int> skins = new List<int>();
    public List<int> trails = new List<int>();
    public List<Level> levels = new List<Level>();

    public AdventureLog()
    {
        levels.Add(new Level("Level 1"));
        levels.Add(new Level("Level 2"));
        levels.Add(new Level("Level 3"));
        levels.Add(new Level("Level 4"));
        levels.Add(new Level("Level 5"));
    }
}
[System.Serializable]
public class Level
{
    public string title;
    public bool completed;
    public bool collectedAllDiamonds;

    public Level(string title)
    {
        this.title = title;
    }
}
