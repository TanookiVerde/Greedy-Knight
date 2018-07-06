using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndLoad : MonoBehaviour {
	private const string SAVE_NAME = "levelData";
	private const string FINISHED_LEVEL = "finishedLevel";
	private const string ERROR = "error";

	public static int[,] LoadLevelData()
	{
		int[,] sample = new int[9,3] { {1,1,1}, {1,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0}, {0,0,0} };
		string s = PlayerPrefs.GetString(SAVE_NAME,ERROR);
		if(s != ERROR)
			return JsonUtility.FromJson<int[,]>(s);
		else
			return sample;
	}
	public static void SaveLevelData(int[,] levelData)
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
}
