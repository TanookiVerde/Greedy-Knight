using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenu : MonoBehaviour {
	[Header("General")]
	[SerializeField] private CanvasGroup mainMenu;
	[SerializeField] private CanvasGroup levelSelectionMenu;
	[Header("Camera Positions")]
	[SerializeField] private LevelSelectionCamera cam;
	[SerializeField] private Transform mainMenuPos;
	[SerializeField] private Transform levelSelectionPos;
	[Header("Transition Preferences")]
	[SerializeField] private Image transition;
	[SerializeField] private float transitionDuration;

	public void StartGame()
	{
		SceneManager.LoadSceneAsync("MainScene");
	}
	public void ExitGame()
	{
		Application.Quit();
	}
	public void OpenLevelSelectionMenu()
	{
		StartCoroutine( ShowLevelSelectionMenu() );
	}
	public void OpenMainMenu()
	{
		StartCoroutine( ShowMainMenu() );
	}
	private IEnumerator ShowLevelSelectionMenu()
	{
		yield return ToggleTransition(true);
		ToggleCanvasGroup(mainMenu,false);
		yield return cam.SetCameraHeight(levelSelectionPos.position.y, 0);
		ToggleCanvasGroup(levelSelectionMenu,true);
		yield return ToggleTransition(false);
	}
	private IEnumerator ShowMainMenu()
	{
		yield return ToggleTransition(true);
		ToggleCanvasGroup(levelSelectionMenu,false);
		yield return cam.SetCameraHeight(mainMenuPos.position.y, 0);
		ToggleCanvasGroup(mainMenu,true);
		yield return ToggleTransition(false);
	}
	private IEnumerator ToggleTransition(bool show)
	{
		float target = show ? 1 : 0;
		transition.DOFillAmount(target, transitionDuration);
		yield return new WaitForSeconds(transitionDuration);
	}
	private void ToggleCanvasGroup(CanvasGroup group, bool active)
	{
		float target = active ? 1 : 0;
		group.DOFade(target, 0);
		group.ignoreParentGroups = active;
		group.interactable = active;
	}
}
