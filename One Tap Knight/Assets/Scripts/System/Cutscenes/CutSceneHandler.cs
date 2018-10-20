using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CutSceneHandler : MonoBehaviour {

	public float slidefadeInDuration = 1;
	public float slideWaitTime = 3;
	public float slidefadeOutDuration = 1;
	public List<CutScene> scenes;

	public Image fadeImage;
	public Image slideImage;
	public TMPro.TMP_Text slideText;

	private IEnumerator current = null;
	private bool playing = false;

	private void Start () 
	{
		InitialSettings();
		test();
	}
	private void Update()
	{
		if(playing)
			Skip();
	}
	private void Skip()
	{
		if(Input.GetMouseButtonDown(0))
		{
			print("skipped");
			StopCoroutine(current);
			playing = false;
		}
	}
	public void InitialSettings()
	{
		fadeImage.color = Color.clear;
		slideImage.color = Color.clear;
		slideText.color = Color.clear;
		slideText.text = "";
		DontDestroyOnLoad(gameObject);
	}
	public void test()
	{
		StartCoroutine(PlayScene("test"));
	}
	public IEnumerator PlayScene(string name)
	{
		foreach(CutScene c in scenes)
		{
			if(c.sceneName == name)
			{
				yield return FadeScreen();
				for(int i = 0; i < c.slides.Count; i++)
				{
					current = FadeSlide(c.slides[i]);
					StartCoroutine(current);
					while(playing)
						yield return null;	
					yield return FadeSlideOut(c.slides[i]);	
				}
			}
		}
		yield return FadeScreen(false);
	}
	public IEnumerator FadeScreen(bool value = true)
	{
		Color c = value ? Color.black : Color.clear;
		fadeImage.DOColor(c, slidefadeOutDuration);
		yield return new WaitForSeconds(slidefadeOutDuration);
	}
	public IEnumerator FadeSlide(Slide slide)
	{
		playing = true;
		slideImage.sprite = slide.sprite;
		slideText.text = slide.text;
		slideText.DOColor(Color.white, slidefadeInDuration); 
		slideImage.DOColor(Color.white, slidefadeInDuration);
		yield return new WaitForSeconds(slidefadeInDuration);

		yield return new WaitForSeconds(slideWaitTime);
		
		playing = false;
	}
	public IEnumerator FadeSlideOut(Slide slide)
	{
		slideText.DOColor(Color.clear, slidefadeOutDuration); 
		slideImage.DOColor(Color.clear, slidefadeOutDuration);
		yield return new WaitForSeconds(slidefadeOutDuration);	
	}
}
