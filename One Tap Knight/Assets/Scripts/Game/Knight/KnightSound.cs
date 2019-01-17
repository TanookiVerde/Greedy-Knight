using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSound : MonoBehaviour {
    public AudioSource source;

    public AudioClip jumpSound;
    public AudioClip dieSound;
    public AudioClip fallSound;

	public void PlaySound(SoundType type)
    {
        print("TOCANDO SOM");
        AudioClip clip = null;
        switch (type)
        {
            case SoundType.JUMP:
                clip = jumpSound;
                break;
            case SoundType.FALL:
                clip = fallSound;
                break;
            case SoundType.DIE:
                clip = dieSound;
                break;
        }
        source.clip = clip;
        source.Play();
    }
}
public enum SoundType
{
    JUMP, FALL, DIE
}
