using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public SOGame gameData;
    public AudioSource source;
    public int lastLevelIndex = -1;

    public void Start()
    {
        var l = FindObjectsOfType<MusicManager>();
        if (l.Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this);
        GetComponent<MusicVolume>().UpdateVolume();
    }
    public void PlayMusic(int levelIndex)
    {
        if(lastLevelIndex != levelIndex)
        {
            source.clip = gameData.levels[levelIndex].backgroundSound;
            lastLevelIndex = levelIndex;
            source.Play();
        }
    }
}
