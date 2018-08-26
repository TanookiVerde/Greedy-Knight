using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviour {


	public List<AudioClip> clips; 

	public int volume = 100;
	private AudioSource myAS;

	void Start()
	{
		GetAudioSource();
		SetVolume(volume);
	}
	void GetAudioSource()
	{
		myAS = GetComponent<AudioSource>();
	}
	public void PlayClip(int clip)
	{
		myAS.clip = clips[clip];
		myAS.Play();
	}
	public void SetVolume(int vol)
	{
		volume = vol;
		myAS.volume = volume / 100f;
	}
}
