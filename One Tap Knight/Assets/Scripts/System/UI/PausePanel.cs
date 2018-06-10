using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour{

    [SerializeField] private CanvasGroup elements;
    [SerializeField] private float duration;

	[HideInInspector] public bool paused;

    private void Start()
    {
        paused = false;
        SetActiveInstant(false);
    }
    public void SetActive(bool active)
    {
        if (active)
        {
            elements.DOFade(1, duration);
            elements.interactable = true;
            elements.blocksRaycasts = true;
        }
        else
        {
            elements.DOFade(0, duration);
            elements.interactable = false;
            elements.blocksRaycasts = false;
        }
    }
    public void SetActiveInstant(bool active)
    {
        if (active)
        {
            elements.DOFade(1, duration);
            elements.interactable = true;
            elements.blocksRaycasts = true;
        }
        else
        {
            elements.DOFade(0, duration);
            elements.interactable = false;
            elements.blocksRaycasts = false;
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync("MainScene");
    }
	public void PauseLevel(bool value)
	{
		paused = value;
		if(value)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}
