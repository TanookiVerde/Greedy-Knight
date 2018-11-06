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

    public float movementDuration;
    public float transitionTime;
    public float camInitialPosition;
    public float camFinalPosition;


    private void Start()
    {
        StartCoroutine(MenuLoop());
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
}
