using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PausePanel : MonoBehaviour{
    private const float SHOW_POS = -240f;
    private const float HIDE_POS = 300f;

    [SerializeField] private CanvasGroup group;
    [SerializeField] private Selectable firstSelected;

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
        RectTransform rect = GetComponent<RectTransform>();
        rect.DOAnchorPosX(active ? SHOW_POS : HIDE_POS, duration);
        group.interactable = active;
    }
    public void RestartLevel()
    {
        Transition.transition.TransiteTo(PlayerPrefs.GetString("levelName"));
    }
    public void BackToMenu()
    {
        Transition.transition.TransiteTo("MainMenu");
        FindObjectOfType<MusicManager>().source.DOFade(0, 0.25f);
        Destroy(FindObjectOfType<MusicManager>().gameObject, 0.25f);
    }
    public void PauseLevel(bool value)
	{
		paused = value;
        SetActive(value);
        FindObjectOfType<KnightController>().Stop(value);
        firstSelected.Select();
    }
}
