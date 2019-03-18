using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {
    public AudioSource source;

	public void PlaySound()
    {
        source.Play();
    }
}
