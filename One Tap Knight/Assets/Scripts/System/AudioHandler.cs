using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour {

	private AudioComponent fx;
	private AudioComponent mus;

	private List<int> effects;
	private bool playing = false;

	void Start()
	{
		GetAComponents();
		effects = new List<int>();
	}
	void GetAComponents()
	{
		fx = transform.Find("Effects").gameObject.GetComponent<AudioComponent>();
		mus = transform.Find("Music").gameObject.GetComponent<AudioComponent>();
	}
	public void SetEffectsVolume(int vol)
	{
		fx.SetVolume(vol);
	}
	public void SetMusicVolume(int vol)
	{
		mus.SetVolume(vol);
	}
	public void PlayEffect(int clip)
	{
		fx.PlayClip(clip);
	}
	public void PlayMusic(int clip)
	{
		mus.PlayClip(clip);
	}
	public void StoreEffect(int clip)
	{
		effects.Add(clip);
	}
	public void PlayStoredEffects()
	{
		if(playing)
		{
			StopCoroutine(PlayStored());
			ResetEffects();
		}
		StartCoroutine(PlayStored());
	}
	IEnumerator PlayStored()
	{
		playing = true;
		for(int i = 0; i < effects.Count; i++)
		{
			fx.PlayClip(effects[i]);
			yield return new WaitForSeconds(fx.clips[effects[i]].length);
		}
		ResetEffects();
		playing = false;
	}
	public void ResetEffects()
	{
		effects = new List<int>();
	}
}
