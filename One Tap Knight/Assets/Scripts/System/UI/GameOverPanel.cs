using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
    public float timeToAppear;

    public CanvasGroup darkBackground;
    public TMP_Text header;
    public Image knight;
    public Image flash;
    public RectTransform retryButton;
    public RectTransform menuButton;

    public void Show()
    {
        StartCoroutine(GameOver());
    }
    private IEnumerator GameOver()
    {
        darkBackground.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        header.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        flash.DOFade(1, 0.25f);
        yield return new WaitForSeconds(0.25f);
        knight.DOFade(1, 0.25f);
        yield return new WaitForSeconds(0.25f);
        retryButton.DOAnchorPosY(retryButton.anchoredPosition.y + 200, 0.10f);
        yield return new WaitForSeconds(0.1f);
        menuButton.DOAnchorPosY(menuButton.anchoredPosition.y + 200, 0.10f);
        darkBackground.interactable = true;
        darkBackground.blocksRaycasts = true;
        retryButton.GetComponent<Button>().Select();
        var log = MemoryCard.Load();
        log.deaths++;
        MemoryCard.Save(log);
    }
    public void Retry()
    {
        Transition.transition.TransiteTo(PlayerPrefs.GetString("levelName"), false);
    }
    public void ToMenu()
    {
        Transition.transition.TransiteTo("MainMenu", true, true);
    }
}
