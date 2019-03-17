using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolume : MonoBehaviour {

    public MusicType type;

    private void Start()
    {
        UpdateVolume();
    }
    public void UpdateVolume()
    {
        if (type == MusicType.MUSIC)
            GetComponent<AudioSource>().volume = MemoryCard.Load().musicVolume / 10f;
        else
            GetComponent<AudioSource>().volume = MemoryCard.Load().sfxVolume / 10f;
        print("Volume::" + GetComponent<AudioSource>().volume);
    }
}
public enum MusicType
{
    MUSIC, SFX
}
