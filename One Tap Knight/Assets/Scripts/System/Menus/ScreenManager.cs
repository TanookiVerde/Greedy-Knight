using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenManager : MonoBehaviour {

	[SerializeField] private List<CanvasGroup> screens;
	[SerializeField] private float fadeDuration;
	private int currentScreen;

	public void Start()
	{
		if(SaveAndLoad.GetFinishedLevel())
		{
			OpenScreen((int)Screen.LEVEL_SELECTION);
		}else{
			OpenScreen((int)Screen.MAIN);
		}
	}
	public void OpenScreen(int newScreen)
	{
		screens[currentScreen].DOFade(0,fadeDuration);
		screens[currentScreen].interactable = false;
		screens[currentScreen].blocksRaycasts = false;
		screens[currentScreen].GetComponent<IScreen>().Close();
		currentScreen = newScreen;
		screens[currentScreen].DOFade(1,fadeDuration);
		screens[currentScreen].interactable = true;
		screens[currentScreen].blocksRaycasts = true;
		screens[currentScreen].GetComponent<IScreen>().Prepare();
	}
}
public enum Screen{
	MAIN = 0,LEVEL_SELECTION = 1,CREDITS = 2
}
