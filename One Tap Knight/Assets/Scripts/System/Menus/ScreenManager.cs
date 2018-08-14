using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScreenManager : MonoBehaviour {

	[SerializeField] private List<CanvasGroup> screens;
	[SerializeField] private float fadeDuration;
	private int currentScreen;

    private const float TIME_BETWEEN_SCREENS = 0.25f;

	public void Start()
	{
		if(SaveAndLoad.GetFinishedLevel())
		{
			OpenScreen((int)SCREEN.LEVEL_SELECTION);
		}else{
			OpenScreen((int)SCREEN.MAIN);
		}
	}
	public void OpenScreen(int newScreen)
	{
        StartCoroutine(IEOpenScreen(newScreen));
	}
    public IEnumerator IEOpenScreen(int newScreen)
    {
        screens[currentScreen].DOFade(0, fadeDuration);
        screens[currentScreen].interactable = false;
        screens[currentScreen].blocksRaycasts = false;
        screens[currentScreen].GetComponent<IScreen>().Close();

        float duration = Transite(newScreen != currentScreen);
        yield return new WaitForSeconds(duration/2);

        currentScreen = newScreen;
        screens[currentScreen].DOFade(1, fadeDuration);
        screens[currentScreen].interactable = true;
        screens[currentScreen].blocksRaycasts = true;
        screens[currentScreen].GetComponent<IScreen>().Prepare();

        yield return new WaitForSeconds(duration);

        yield return screens[currentScreen].GetComponent<IScreen>().BeginningAnimation();

    }
    private float Transite(bool hasFirstHalf)
    {
        var transition = GameObject.Find("_TRANSITION").GetComponent<Transition>();
        transition.ToggleTransition(hasFirstHalf);
        return transition.TRANSITION_DURATION;
    }
}
public enum SCREEN{
	MAIN = 0,LEVEL_SELECTION = 1,CREDITS = 2
}
