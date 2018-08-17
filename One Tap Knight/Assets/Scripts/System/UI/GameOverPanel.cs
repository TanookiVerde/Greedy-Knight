using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour {
    private const float TIME_TO_SHOW_BG = 0.3f;
    private const float TIME_TO_SHOW_TITLE = 0.5f;
    private const float TIME_TO_SHOW_BUTTONS = 0.5f;
    private const float TIME_TO_SHOW_FLASH = 1f;
    private const float DELAY_TO_START_ANIMATION = 1f;
    private const float DELAY_BETWEEN_BUTTONS = 0.25f;

    [SerializeField] private Image background;
    [SerializeField] private CanvasGroup flash;
    [SerializeField] private Text title;
    [SerializeField] private CanvasGroup retryButton;
    [SerializeField] private CanvasGroup menuButton;

    [SerializeField] private CanvasGroup skipAnimation;

    private void Start()
    {
        skipAnimation.interactable = false;
        skipAnimation.blocksRaycasts = false;
    }
    public IEnumerator Appear()
    {
        yield return new WaitForSeconds(DELAY_TO_START_ANIMATION);
        background.DOFillAmount(1, TIME_TO_SHOW_BG);
        skipAnimation.interactable = true;
        skipAnimation.blocksRaycasts = true;
        yield return new WaitForSeconds(TIME_TO_SHOW_BG);
        title.transform.DOScale(1, TIME_TO_SHOW_TITLE);
        flash.DOFade(1, TIME_TO_SHOW_TITLE);
        yield return new WaitForSeconds(TIME_TO_SHOW_FLASH);
        retryButton.interactable = true;
        menuButton.interactable = true;
        retryButton.blocksRaycasts = true;
        menuButton.blocksRaycasts = true;
        retryButton.transform.DOScale(new Vector3(1f, 1f, 1f), TIME_TO_SHOW_BUTTONS);
        yield return new WaitForSeconds(DELAY_BETWEEN_BUTTONS);
        skipAnimation.interactable = false;
        skipAnimation.blocksRaycasts = false;
        menuButton.transform.DOScale(new Vector3(1f, 1f, 1f), TIME_TO_SHOW_BUTTONS);
        yield return new WaitForSeconds(TIME_TO_SHOW_BUTTONS);
    }
    public void JumpGameOverAnimation()
    {
        skipAnimation.interactable = false;
        skipAnimation.blocksRaycasts = false;
        background.DOFillAmount(1, 0);
        title.transform.DOScale(1, 0);
        flash.DOFade(1, 0);
        retryButton.interactable = true;
        menuButton.interactable = true;
        retryButton.blocksRaycasts = true;
        menuButton.blocksRaycasts = true;
        retryButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0);
        menuButton.transform.DOScale(new Vector3(1f, 1f, 1f), 0);
    }
    public void DisableGameOverPanel()
    {
        background.DOFillAmount(0, 0);
        title.transform.DOScale(0, 0);
        flash.DOFade(0, 0);
        retryButton.transform.DOScale(0, 0);
        menuButton.transform.DOScale(0, 0);
        retryButton.interactable = false;
        menuButton.interactable = false;
        retryButton.blocksRaycasts = false;
        menuButton.blocksRaycasts = false;
    }
    public void RetryLevel()
    {
        Scene level = SceneManager.GetActiveScene();
        SceneManager.LoadScene(level.buildIndex);
    }
    public void BackToMenu()
    {
        SaveAndLoad.SetFinishedLevel(false);
        SaveAndLoad.SetLastOpenedLevel(-1);
        SceneManager.LoadScene("MainMenu");
    }
}
