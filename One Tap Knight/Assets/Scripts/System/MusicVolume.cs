using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public IEnumerator FadeOut(float duration = 1f)
    {
        float volume = MemoryCard.Load().musicVolume/10f;
        print(volume);
        yield return DOTween.To(() => volume, x => volume = x, 0, duration).OnUpdate(
            () => GetComponent<AudioSource>().volume = volume);
    }
    public IEnumerator FadeOutDestroy(float duration = 1f)
    {
        float volume = MemoryCard.Load().musicVolume/10f;
        print(volume);
        yield return DOTween.To(() => volume, x => volume = x, 0, duration).OnUpdate(
            () => GetComponent<AudioSource>().volume = volume).OnComplete(() => Destroy(gameObject));
    }
}
public enum MusicType
{
    MUSIC, SFX
}
