﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DG.Tweening;

public class Darkness : MonoBehaviour {

	public List<TilemapRenderer> darkenedTiles;
	public List<SpriteRenderer> undarkenedObjects;
	public float fadeDuration = 1;

	private bool darkened = false;
	
	public void Activate()
	{
		if(darkened)
			Undarken();
		else
			Darken();

		darkened = !darkened;
	}
	void Darken()
	{
		foreach(TilemapRenderer t in darkenedTiles)
		{
			t.material.DOColor(Color.clear, fadeDuration);
		}
	}
	void Undarken()
	{
		foreach(TilemapRenderer t in darkenedTiles)
		{
			t.material.DOColor(Color.white, fadeDuration);
		}
	}
}
