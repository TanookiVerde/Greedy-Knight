using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MusicHandler : MonoBehaviour {

	private AudioSource audioSource;

	[SerializeField] private AudioClip backgroundClip;

	private float normalPitch;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}
	public void PlayClip()
	{
		audioSource.clip = backgroundClip;
		audioSource.Play();
	}
	public void ChangePitch(float newPitch)
	{
		audioSource.DOPitch(newPitch, 1f);
	}
	public void ResetPitch()
	{
		audioSource.DOPitch(normalPitch, 1f);
	}
}
