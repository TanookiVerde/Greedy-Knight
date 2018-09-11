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


	private void Start () 
	{
		InitialSettings();
	}
	public void InitialSettings()
	{
		fadeImage.color = Color.clear;
		slideImage.color = Color.clear;
		slideText.color = Color.clear;
		slideText.text = "";
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
					yield return FadeSlide(c.slides[i]);				
				}
			}
		}
	}
	public IEnumerator FadeScreen(bool value = true)
	{
		Color c = value ? Color.black : Color.clear;
		fadeImage.DOColor(c, slidefadeOutDuration);
		yield return new WaitForSeconds(slidefadeOutDuration);
	}
	public IEnumerator FadeSlide(Slide slide)
	{
		slideImage.sprite = slide.sprite;
		slideText.text = slide.text;
		slideText.DOColor(Color.white, slidefadeInDuration); 
		slideImage.DOColor(Color.white, slidefadeInDuration);
		yield return new WaitForSeconds(slidefadeInDuration);

		yield return new WaitForSeconds(slideWaitTime);
		
		slideText.DOColor(Color.clear, slidefadeOutDuration); 
		slideImage.DOColor(Color.clear, slidefadeOutDuration);
		yield return new WaitForSeconds(slidefadeOutDuration);
	}
}
