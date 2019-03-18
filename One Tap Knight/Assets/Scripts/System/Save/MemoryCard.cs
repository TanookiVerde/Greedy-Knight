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
        return JsonUtility.FromJson<AdventureLog>(File.ReadAllText(GetSavePath()));
    }
    public static void Save(AdventureLog log)
    {
        CreateFileIfDontExist(GetSavePath(), JsonUtility.ToJson(log));
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
    public bool initialized = false;
    public int deaths = 0;
    public int musicVolume = 7;
    public int sfxVolume = 7;

    public List<Level> levels = new List<Level>();

    public AdventureLog()
    {
        levels.Add(new Level("Floresta dos Cogumelos", 1));
        levels.Add(new Level("Caverna Espinhosa", 2));
        levels.Add(new Level("Castelo de Vhalor", 3));
    }
}
[System.Serializable]
public class Level
{
    public string title;
    public int number;
    public bool completed;
    public int diamondsCollected;

    public Level(string title, int number)
    {
        this.title = title;
        this.number = number;
    }
}
