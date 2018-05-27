using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour {
	[SerializeField] private CanvasGroup transition;

	private void Start()
	{
		transition.alpha = 0;
		transition.blocksRaycasts = false;
		transition.interactable = false;
	}
	public void StartGame()
	{
		transition.DOFade(1, 0.5f);
		transition.blocksRaycasts = true;
		transition.interactable = true;
		SceneManager.LoadSceneAsync("MainScene");
	}
	public void ExitGame()
	{
		Application.Quit();
	}
}
