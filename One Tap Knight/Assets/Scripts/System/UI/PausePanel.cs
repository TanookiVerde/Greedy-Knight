using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour{

    [SerializeField] private CanvasGroup group;
    [SerializeField] private Selectable firstSelected;
    [SerializeField] private Toggle tutorialToggle;

	[HideInInspector] public bool paused;

    private void Start()
    {
        paused = false;
        SetActive(false, 0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseLevel(!paused);
    }
    public void SetActive(bool active, float duration = 0.25f)
    {
        group.DOFade(active ? 1 : 0, duration);
        group.interactable = active;
        group.blocksRaycasts = active;
        tutorialToggle.isOn = PlayerPrefs.GetInt("tutorial", 1) == 1 ? true : false;
    }
    public void RestartLevel()
    {
        Transition.transition.TransiteTo(PlayerPrefs.GetString("levelName"));
    }
	public void PauseLevel(bool value)
	{
		paused = value;
        SetActive(value);
        FindObjectOfType<KnightController>().Stop(value);
        firstSelected.Select();
    }
}
