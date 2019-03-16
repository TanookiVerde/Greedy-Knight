using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MainMenu : MonoBehaviour {
    private const float SCREEN_WIDTH = 1280;
    private const float SCREEN_HEIGHT = 720;

    public new Transform camera;
    public CanvasGroup transition;

    public RectTransform logo;
    public List<RectTransform> buttons;
    public RectTransform logoFinalPosition;
    public TMP_Text touchToStart;

    public List<MenuPanel> menus;
    private int currentPanel;

    public float movementDuration;
    public float transitionTime;
    public float camInitialPosition;
    public float camFinalPosition;

    private bool opened;

    private void Start()
    {
        Transition.transition.InstaShow();
        Transition.transition.TransiteFrom();
        Application.targetFrameRate = 60;
        StartCoroutine(MenuLoop());
        MemoryCard.Load();
    }
    private IEnumerator MenuLoop()
    {
        while (true)
        {
            transition.DOFade(0, transitionTime);
            camera.DOMoveX(camInitialPosition, 0);
            camera.DOMoveX(camFinalPosition, movementDuration).SetEase(Ease.Linear);
            yield return new WaitForSeconds(movementDuration - transitionTime);
            transition.DOFade(1, transitionTime);
            yield return new WaitForSeconds(transitionTime);
        }
    }
    public void OpenMenu()
    {
        if (!opened)
        {
            touchToStart.DOFade(0, 0.25f);
            logo.DOAnchorPos(logoFinalPosition.anchoredPosition, 0.25f);
            logo.DOScale(logoFinalPosition.localScale, 0.25f);
            logo.DORotate(logoFinalPosition.rotation.eulerAngles, 0.25f);
            for (int i = 0; i < buttons.Count; i++) 
            {
                var pos = buttons[i].anchoredPosition.x;
                buttons[i].DOAnchorPosX(pos - SCREEN_WIDTH, 0.25f + i * 0.15f);
            }
            opened = true;
        }
    }
    public void OpenPanel(int i)
    {
        menus[i].Open();
    }
}
