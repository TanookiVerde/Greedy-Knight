using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
	[Header("UI Objects")]
	[SerializeField] private CanvasGroup header;
	[SerializeField] private CanvasGroup retryButtom;
	[SerializeField] private RectTransform frame;
	[SerializeField] private Text headerText;

	[Header("Preferences")]
	[SerializeField] private float duration = 0.3f;

	public static GameOverPanel gameOverPanel;

	private void Start()
	{
		HideGameOverPanel(0);
		GameOverPanel.gameOverPanel = this;
	}
	public void ShowGameOverPanel()
	{
		StartCoroutine(ShowGameOverPanelAnimation());
	}
	public void HideGameOverPanel(float duration)
	{
		frame.DOScaleY(0,duration/2);
		header.DOFade(0,duration/2);
		retryButtom.DOFade(0,duration/2);
	}
	public IEnumerator ShowGameOverPanelAnimation()
	{
		frame.DOScaleY(1,duration);
		yield return new WaitForSeconds(duration);
		headerText.text = (PlayerPrefs.GetInt("deathCount",1)-1).ToString();
		header.DOFade(1,duration/2);
		headerText.DOFade(1,duration/2);
		yield return ChangeDeathCount();
		retryButtom.DOFade(1,duration/2);
	}
	private IEnumerator ChangeDeathCount()
	{
		headerText.transform.DOShakeRotation(duration);
		yield return new WaitForSeconds(duration);
		headerText.text = PlayerPrefs.GetInt("deathCount",1).ToString();
		headerText.transform.DOShakeRotation(duration);
	}
	public void RestartLevel()
	{
		SceneManager.LoadSceneAsync("MainScene");
	}
	public void BackToMenu()
	{
		SceneManager.LoadSceneAsync("MainMenu");
	}
}
