using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioComponent : MonoBehaviour {


	public List<AudioClip> clips; 

	public int volume = 100;

	private GameObject audioPlayer;

	void Start()
	{
		GetAudioSource();
		SetVolume(volume);
	}
	void GetAudioSource()
	{
		audioPlayer = new GameObject();
		audioPlayer.AddComponent<AudioSource>();
	}
	public void PlayClip(int clip)
	{
		GameObject g = Instantiate(audioPlayer);
		g.GetComponent<AudioSource>().volume = volume;
		g.GetComponent<AudioSource>().clip = clips[clip];
		g.GetComponent<AudioSource>().Play();

		Destroy(g, clips[clip].length);
	}
	public void SetVolume(int vol)
	{
		volume = vol;
	}
}
